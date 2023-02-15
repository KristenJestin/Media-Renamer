using MediaRenamer.Media.Models;
using MediaRenamer.Services;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace MediaRenamer.Commands;

public sealed class BatchCommand : AsyncCommand<BatchCommand.Settings>
{
    //private readonly ILogger<HelloCommand> _logger;
    private readonly IAnsiConsole _console;
    private readonly FileService _fileService;
    private readonly MediaDataService _mediaService;

    public BatchCommand(IAnsiConsole console/*, ILogger<HelloCommand> logger*/, FileService fileService, MediaDataService mediaService)
    {
        _console = console;
        _fileService = fileService;
        _mediaService = mediaService;
    }

    public sealed class Settings : CommandSettings
    {
        [CommandOption("-n|--name <NAME>")]
        [Description("The person or thing to greet.")]
        [DefaultValue("World")]
        public string Name { get; set; }
    }


    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var medias = AnsiConsole.Status()
            .Start("Retriving files...", ctx => _fileService.GetMediaFilesFromSource());
        _console.MarkupLine($"[cyan]{medias.Count()} files founded[/]");

        foreach (var media in medias)
        {
            if (media.Data.Type == MediaType.Tv)
            {
                var result = await _mediaService.SearchTvAsync(media);
                var test = "";
            }
            else if (media.Data.Type == MediaType.Movie)
            {
                var result = await _mediaService.SearchMovieAsync(media);
                var test = "";
            }
        }

        return 0;
    }
}