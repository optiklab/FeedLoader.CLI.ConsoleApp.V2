using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using VoxSmart.Feed.App.Cli;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using VoxSmart.Feed.Common;
using VoxSmart.Feed.App;
using System.CommandLine.NamingConventionBinder;
using VoxSmart.Feed.App.CommandHandlers;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var root = new RootCommand();
        root.Description = "VoxSmart CLI app to work with Financial feeds.";

        root.AddCommand(new ReadUrlFeedCommand());
        root.Handler = CommandHandler.Create(() => root.Invoke(args));

        return await new CommandLineBuilder(root)
           .UseHost(builder => builder
                .ConfigureServices(RegisterServices)
                .UseCommandHandler<ReadUrlFeedCommand, ReadUrlFeedCommandHandler>())
           .UseDefaults()
           //.UseCustomErrorHandler(ExceptionHook)
           .Build()
           .InvokeAsync(args);
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddSingleton<IFeedParser, FeedParser>(); // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
                                                          // https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
        services.AddSingleton<IFeedIntegrationManager, ADjFeedIntegrationManager>();
        services.AddSingleton<IConsoleOutputHandlers, ConsoleOutputHandlers>();
    }

    //public static Action<Exception> ExceptionHook { get; set; }
}