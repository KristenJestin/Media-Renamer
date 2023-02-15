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
using Spectre.Console.Cli;
using System.Reflection;

var dir = new DirectoryInfo(@"C:\Users\krisj\Downloads\test");
var assembly = Assembly.GetEntryAssembly()!;

// Create a type registrar and register any dependencies.
// A type registrar is an adapter for a DI framework.
var services = new ServiceCollection();

// Configuration
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configurationBuilder = new ConfigurationBuilder()
        .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .AddUserSecrets(assembly);
var configuration = configurationBuilder.Build();

var secrets = configuration.GetSection(nameof(AppSecrets));
services.Configure<AppConfig>(configuration.GetSection(nameof(AppConfig)));
services.Configure<AppSecrets>(secrets);

// Singletons
services.AddSingleton(new TMDbClient(secrets.Get<AppSecrets>()?.ApiKeyTmdb ?? throw new Exception("You should provide an api key in the secrets")));
services.AddSingleton<FileService>();
services.AddSingleton<MediaDataService>();

// Flurl
FlurlHttp.Configure(settings => {
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