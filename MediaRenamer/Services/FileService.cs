using MediaRenamer.Common.Exceptions;
using MediaRenamer.Common.Extensions;
using MediaRenamer.Media.Models;
using MediaRenamer.Models;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace MediaRenamer.Services;

public class FileService
{
	private readonly AppConfig _config;

	public FileService(IOptions<AppConfig> config)
	{
		_config = config.Value;
	}

	public IEnumerable<MediaData> GetMediaFilesFromSource(DirectoryInfo directory)
	{
		var files = directory
            .EnumerateFiles("*.*", _config.OnlyTopDirectory ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories)
			.Where(file => _config.GetMask().Contains(file.Extension));

		return files.Select(MediaData.Create);
	}

	public async Task CopyFileAsync(FileInfo source, string fileName, Action<double, CancelEventArgs>? onProgressChanged = null, bool overwrite = false)
	{
		if (source.DirectoryName == null)
			throw new ArgumentNullException(nameof(source));

		var destination = Path.Combine(source.DirectoryName, fileName, source.GetExtension());

		// check if already exists
		if (!overwrite && File.Exists(destination))
			throw new FileExistsException($"The destination already exists (enable {overwrite} to replace it).", fileName);

		var buffer = new byte[1024 * 1024]; // 1MB buffer
		var cancel = new CancelEventArgs();

		using var sourceStream = new FileStream(source.FullName, FileMode.Open, FileAccess.Read);
		var fileLength = sourceStream.Length;
		using var destinationStream = new FileStream(destination, overwrite ? FileMode.Create : FileMode.CreateNew, FileAccess.Write);
		long totalBytes = 0;
		var currentBlockSize = 0;

		// TODO: use cancelation token on Task
		while ((currentBlockSize = await sourceStream.ReadAsync(buffer)) > 0)
		{
			totalBytes += currentBlockSize;
			var percentage = totalBytes * 100.0 / fileLength;

			// TODO: use cancelation token on Task
			await destinationStream.WriteAsync(buffer.AsMemory(0, currentBlockSize));

			onProgressChanged?.Invoke(percentage, cancel);

			if (cancel.Cancel)
			{
				File.Delete(destination);
				break;
			}
		}
	}
}
