using Lanysom.DataContextBuilder.Memory;
using Lanysom.DataContextBuilder.SqlServer;

namespace Lanysom.DataContextBuilder
{
    public static class DataContextBuilderExtensions
    {
        public static DataContextBuilder SqlServer(this SupportedPlatforms platforms, string connectionString)
        {
            return new SqlServerDataContextBuilder(connectionString);
        }

        public static DataContextBuilder Memory(this SupportedPlatforms platforms)
        {
            return new MemoryDataContextBuilder();
        }
    }
}
