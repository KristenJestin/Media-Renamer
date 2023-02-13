using MediaRenamer.Managers;
using MediaRenamer.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace MediaRenamer;

public class ApplicationHostService : IHostedService
{
    private readonly AppConfig _config;

    public ApplicationHostService(IOptions<AppConfig> option)
    {
        _config = option.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var files = _config.GetFilesFromSource();
        foreach (var file in files)
        {
            var test = "";
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
