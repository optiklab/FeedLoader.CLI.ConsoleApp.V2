using System.CommandLine;

namespace VoxSmart.Feed.App.Cli
{
    /// <summary>
    /// Initialize commands patterns using dotnet functionality
    /// https://learn.microsoft.com/en-us/dotnet/standard/commandline/get-started-tutorial
    /// https://learn.microsoft.com/en-us/dotnet/standard/commandline/
    /// https://nikiforovall.github.io/dotnet/cli/2021/06/06/clean-cli.html
    /// 
    /// NOTE: In case it would have much more commands, I would split it to classes and implemented some Reflection-based mechanism to automatically register commands.
    /// </summary>

    public class ReadUrlFeedCommand : Command
    {
        public ReadUrlFeedCommand()
            : base(name: "--read-url-feed", "Reads financial entity names from the feed XML available via specified URL.")
        {
            var urlFeedAgrument = new Argument<Uri>("url", "URI to download.");

            AddArgument(urlFeedAgrument);
        }
    }
}
