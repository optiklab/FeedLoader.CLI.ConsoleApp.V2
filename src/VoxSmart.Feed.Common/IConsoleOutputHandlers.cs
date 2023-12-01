namespace VoxSmart.Feed.Common
{
    public interface IConsoleOutputHandlers
    {
        Task<string> ReadUrlAsync(Uri uri);
    }
}
