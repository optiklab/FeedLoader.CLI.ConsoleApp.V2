using System.CommandLine.Invocation;
using VoxSmart.Feed.Common;

namespace VoxSmart.Feed.App.CommandHandlers
{
    public class ReadUrlFeedCommandHandler : ICommandHandler
    {
        private readonly IConsoleOutputHandlers _consoleOutputHandlers;

        public Uri? Url { get; set; }

        public ReadUrlFeedCommandHandler(IConsoleOutputHandlers consoleOutputHandlers)
        {
            _consoleOutputHandlers = consoleOutputHandlers ?? throw new ArgumentNullException(nameof(consoleOutputHandlers));
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            Console.WriteLine(await _consoleOutputHandlers.ReadUrlAsync(Url));

            return 0;
        }

        public int Invoke(InvocationContext context)
        {
            Console.WriteLine(_consoleOutputHandlers.ReadUrlAsync(Url).ConfigureAwait(false));

            return 0;
        }
    }
}
