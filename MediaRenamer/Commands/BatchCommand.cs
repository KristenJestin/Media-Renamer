using Humanizer;
using MediaRenamer.Common.Exceptions;
using MediaRenamer.Media.Models;
using MediaRenamer.Models;
using MediaRenamer.Services;
using Microsoft.Extensions.Options;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MediaRenamer.Commands;

public sealed class BatchCommand : AsyncCommand<BatchCommand.Settings>
{
    //private readonly ILogger<HelloCommand> _logger;
    private readonly AppConfig _config;
    private readonly IAnsiConsole _console;
    private readonly FileService _fileService;
    private readonly MediaService _mediaService;

    public BatchCommand(IOptions<AppConfig> config, IAnsiConsole console/*, ILogger<HelloCommand> logger*/, FileService fileService, MediaService mediaService)
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

        [CommandOption("--debug")]
        public bool? Debug { get; set; }



        public override ValidationResult Validate()
        {
            if (!Directory.Exists(SourcePath))
                return ValidationResult.Error("You must provide an existing source path.");
            return ValidationResult.Success();
        }
    }


    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var source = new DirectoryInfo(settings.SourcePath);

        var medias = AnsiConsole.Status()
            .Start("Retriving files...", ctx => _fileService.GetMediaFilesFromSource(source));
        _console.MarkupLine($"[italic cyan]{medias.Count()} files founded[/]");
        _console.WriteLine();

        foreach (var media in medias)
        {
            _console.MarkupLine($"[cyan]Searching for {Markup.Escape(media.File.Name.Truncate(40, "..."))}[/]");

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
                        if (settings.Debug != true)
                            await _fileService.CopyFileAsync(media.File, moving, onProgressChanged: (progress, _) => task.Value = progress, overwrite: _config.Overwrite);
                        _console.MarkupLine($"[bold green] == Moved has '{Markup.Escape(moving.FileName.Truncate(40, "..."))}'[/]");
                        if (settings.Debug != true)
                            media.File.Delete();
                    }
                    catch (FileExistsException file)
                    {
                        _console.MarkupLine($"[red]Media '{Markup.Escape(file.FileName ?? "")}' cannot be moved because it already exists[/]");
                    }

                    _console.WriteLine();
                });
        }

        return 0;
    }
}