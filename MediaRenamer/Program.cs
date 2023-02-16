using FluentValidation.Results;
using Flurl.Http;
using Flurl.Http.Configuration;
using MediaRenamer.Commands;
using MediaRenamer.Common.Infrastructure;
using MediaRenamer.Models;
using MediaRenamer.Services;
using MediaRenamer.TMDb.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Reflection;

var dir = new DirectoryInfo(@"C:\Users\krisj\Downloads\test");
var assembly = Assembly.GetEntryAssembly()!;

// Create a type registrar and register any dependencies.
// A type registrar is an adapter for a DI framework.
var services = new ServiceCollection();

// Configuration
var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");
var configurationBuilder = new ConfigurationBuilder()
        .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .AddUserSecrets(assembly);
var configuration = configurationBuilder.Build();

var secrets = configuration.GetSection(nameof(AppSecrets));
var appConfig = configuration.GetSection(nameof(AppConfig));
var appConfigValue = appConfig.Get<AppConfig>();
var secretsValue = secrets.Get<AppSecrets>();

services.Configure<AppConfig>(appConfig);
services.Configure<AppSecrets>(secrets);

// validation
if (appConfigValue == null)
    return ShowError(exception: new Exception("Configuration file hasn't been provided."));

if (secretsValue == null)
    return ShowError(exception: new Exception("You should provide an api key in the secrets."));

if (!appConfigValue.TryValidate(out var errorsAppConfig))
    return ShowError(errors: errorsAppConfig);

if (!secretsValue.TryValidate(out var errorsSecrets))
    return ShowError(errors: errorsAppConfig);

// Singletons
services.AddSingleton(new TMDbClient(secretsValue.ApiKeyTMDb, language: appConfigValue.Langugage));
services.AddSingleton<FileService>();
services.AddSingleton<MediaDataService>();

// Flurl
FlurlHttp.Configure(settings =>
{
    var jsonSettings = new JsonSerializerSettings
    {
        ObjectCreationHandling = ObjectCreationHandling.Replace
    };
    settings.JsonSerializer = new NewtonsoftJsonSerializer(jsonSettings);
});

// DI
var registrar = new TypeRegistrar(services);

// Create a new command app with the registrar
// and run it with the provided arguments.
var app = new CommandApp(registrar);
app.Configure(config =>
{
    config.AddCommand<BatchCommand>("batch");
});


return await app.RunAsync(args);

static int ShowError(string? message = null, Exception? exception = null, IEnumerable<ValidationFailure>? errors = null)
{
    AnsiConsole.MarkupLine($"[bold red]Errors occurred while retrieving the configuration.[/]");
    AnsiConsole.MarkupLine($"[red]The program cannot start.[/]");

    if (message != null || exception != null || (errors != null && errors.Any()))
    {
        AnsiConsole.WriteLine();
        if (message != null)
            AnsiConsole.MarkupLine(Markup.Escape(message));
        else if (exception != null)
            AnsiConsole.WriteException(exception);
        else if (errors != null)
        {
            var table = new Table();

            // Add some columns
            table.AddColumns("Property", "Error");
            foreach (var error in errors)
                table.AddRow(Markup.Escape(error.PropertyName), $"[red]{Markup.Escape(error.ErrorMessage)}[/]");

            AnsiConsole.Write(table);
        }
    }

    return 1;
}