using Cronos;
using Humanizer;
using MediaRenamer.Common;
using MediaRenamer.Common.Exceptions;
using MediaRenamer.Media.Models;
using MediaRenamer.Models;
using MediaRenamer.Services;
using Microsoft.Extensions.Options;
using Realms;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MediaRenamer.Commands;

public sealed class BatchCommand : AsyncCommand<BatchCommand.Settings>
{
    private readonly AppConfig _config;
    private readonly IAnsiConsole _console;
    private readonly FileService _fileService;
    private readonly MediaService _mediaService;

    public BatchCommand(IOptions<AppConfig> config, IAnsiConsole console, FileService fileService, MediaService mediaService)
    {
        _config = config.Value;
        _console = console;
        _fileService = fileService;
        _mediaService = mediaService;
    }

    public sealed class Settings : CommandSettings
    {
        [Description("Media file file path to process")]
        [CommandArgument(0, "<SOURCE>")]
        public string SourcePath { get; set; }

        public override ValidationResult Validate()
        {
            if (SourcePath == null || !Directory.Exists(SourcePath))
                return ValidationResult.Error("You must provide an existing source path.");
            return ValidationResult.Success();
        }
    }


    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        if (_config.CronExpression != null)
        {
            var timer = new CronPeriodicTimer(_config.CronExpression);
            while (await timer.WaitForNextTickAsync())
                await StartRenamingAsync(settings);
        }
        else
        {
            await StartRenamingAsync(settings);
        }

        return 0;
    }


    #region privates
    public async Task StartRenamingAsync(Settings settings)
    {
        var source = new DirectoryInfo(settings.SourcePath);

        var medias = _console.Status()
            .Start("Retriving files...", ctx => _fileService.GetMediaFilesFromSource(source));
        _console.MarkupLine($"[italic cyan]{medias.Count()} files found[/]");
        _console.WriteLine();

        foreach (var media in medias)
        {
            _console.MarkupLine($"[cyan]Searching for {Markup.Escape(media.File.Name.Truncate(100, "..."))}[/]");

            media.SetData(await _mediaService.SearchTvMovieAsync(media));
            if (media.Data == null)
            {
                _console.MarkupLine($"[italic red] == No match found ({Markup.Escape(media.ExtractedData.Title)}) [/]");
                _console.WriteLine();
                continue;
            }

            // build name
            var moving = _fileService.BuildMovingMedia(media);

            // move
            await AnsiConsole.Progress()
                .AutoClear(true)   // Hide tasks as they are completed
                .Columns(new ProgressColumn[]
                {
                    new TaskDescriptionColumn(),    // Task description
                    new ProgressBarColumn(),        // Progress bar
                    new PercentageColumn(),         // Percentage
                    new RemainingTimeColumn(),      // Remaining time
                    new SpinnerColumn(),            // Spinner
                })
                .StartAsync(async ctx =>
                {
                    var task = ctx.AddTask($"[green]moving {Markup.Escape(moving.FileName)}[/]");
                    try
                    {
                        await _fileService.CopyFileAsync(media.File, moving, onProgressChanged: (progress, _) => task.Value = progress, overwrite: _config.Overwrite);
                        _console.MarkupLine($"[bold green] == Moved has '{Markup.Escape(moving.FileName.Truncate(100, "..."))}'[/]");
                        media.File.Delete();

                        // save history
                        using var realm = Realm.GetInstance(Constants.RealmConfiguration);
                        await realm.WriteAsync(() => realm.Add(new MovingHistory(media, moving)));
                    }
                    catch (FileExistsException file)
                    {
                        _console.MarkupLine($"[red]Media '{Markup.Escape(file.FileName ?? "")}' cannot be moved because it already exists (set {nameof(AppConfig.Overwrite)} to \"{true}\" to replace it)[/]");
                    }

                    _console.WriteLine();
                });
        }
    }
    #endregion
}