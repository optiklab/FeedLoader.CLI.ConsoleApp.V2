using System.Text;
using VoxSmart.Feed.Common;
using VoxSmart.Feed.Common.Model;

namespace VoxSmart.Feed.App.Cli
{
    /// <summary>
    /// This is fully static class that incapsulates the high-level logic needed for the expected console output.
    /// </summary>
    public class ConsoleOutputHandlers : IConsoleOutputHandlers
    {
        private IFeedIntegrationManager _feedIntegrationManager;

        public ConsoleOutputHandlers(IFeedIntegrationManager feedIntegrationManager)
        {
            _feedIntegrationManager = feedIntegrationManager;
        }

        public async Task<string> ReadUrlAsync(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            // Load an parse.
            VoxSmartRss result = await _feedIntegrationManager.LoadFromUriAsync(uri);

            // Form output.
            var sb = new StringBuilder();
            sb.AppendLine($"Extracted {result.Entities.Count} entities:");
            for (int counter = 0; counter < result.Entities.Count; counter++)
            {
                sb.AppendLine($"{counter} {result.Entities[counter].Title}"); // If more data is needed to output, then we can make an extension method for formatting?
            }

            return sb.ToString();
        }
    }
}
