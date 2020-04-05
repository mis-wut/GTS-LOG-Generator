using System.IO;

namespace GTSLogGeneratorApi.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool IsDirectoryExists(this string path)
        {
            return Directory.Exists(path);
        }
    }
}