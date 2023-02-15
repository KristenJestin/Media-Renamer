using MediaRenamer.Models;
using MediaRenamer.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Spectre.Console;

namespace MediaRenamer;

public class ApplicationHostService : IHostedService
{
    private readonly AppConfig _config;
    private readonly FileService _fileService;
    private readonly MediaDataService _mediaService;

    public ApplicationHostService(IOptions<AppConfig> option, FileService fileService, MediaDataService mediaService)
    {
        _config = option.Value;
        _fileService = fileService;
        _mediaService = mediaService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var medias = AnsiConsole.Status()
            .Start("Retriving files...", ctx => _fileService.GetMediaFilesFromSource());
        AnsiConsole.MarkupLine($"[cyan]{medias.Count()} files founded[/]");

        foreach (var media in medias)
        {
            if (media.Data.Type == Models.Medias.MediaType.Tv)
            {
                var result = await _mediaService.SearchTvAsync(media);
                var test = "";
            }
            else if (media.Data.Type == Models.Medias.MediaType.Movie)
            {
                var result = await _mediaService.SearchMovieAsync(media);
                var test = "";
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
