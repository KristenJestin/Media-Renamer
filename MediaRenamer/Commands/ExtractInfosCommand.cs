using MediaRenamer.Common;
using MediaRenamer.Models;
using MediaRenamer.Services;
using Microsoft.Extensions.Options;
using Spectre.Console.Cli;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaRenamer.Media.Models;
using System.Xml.Linq;
using Spectre.Console.Json;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using Flurl.Http.Configuration;
using Newtonsoft.Json.Converters;

namespace MediaRenamer.Commands;

public sealed class ExtractInfosCommand : Command<ExtractInfosCommand.Settings>
{
    private readonly AppConfig _config;
    private readonly IAnsiConsole _console;

    public ExtractInfosCommand(IOptions<AppConfig> config, IAnsiConsole console)
    {
        _config = config.Value;
        _console = console;
    }

    public sealed class Settings : CommandSettings
    {
        [Description("filename to extract data")]
        [CommandArgument(0, "<FILE_NAME>")]
        public string FileName { get; set; }



        public override ValidationResult Validate()
        {
            if (string.IsNullOrWhiteSpace(FileName))
                return ValidationResult.Error("You must provide a filename");
            return ValidationResult.Success();
        }
    }


    public override int Execute(CommandContext context, Settings settings)
    {
        var media = new MediaFile(new FileInfo(settings.FileName + ".mkv"), _config.BeforeReplacements);
        var json = new JsonText(JsonConvert.SerializeObject(media.ExtractedData, Formatting.Indented, new StringEnumConverter()));

        _console.Write(new Panel(json)
            .Collapse()
            .RoundedBorder()
            .BorderColor(Color.Yellow));

        return 0;
    }
}