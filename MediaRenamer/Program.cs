using MediaRenamer;
using MediaRenamer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var dir = new DirectoryInfo(@"C:\Users\krisj\Downloads\test");

AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

var currentLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!;
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(c => c.SetBasePath(currentLocation))
    .ConfigureServices((context, services) =>
    {
        // App Host
        services.AddHostedService<ApplicationHostService>();

        // Configuration
        services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
    })
    .Build();

await host.RunAsync();

static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
{
    Console.WriteLine(e.ExceptionObject.ToString());
    Console.WriteLine("Press Enter to continue");
    Console.ReadLine();
    Environment.Exit(1);
}