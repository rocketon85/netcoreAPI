using netcoreAPI.Dal;
using netcoreAPI.Helper;

namespace netcoreAPI.Tests.Options
{
    [CollectionDefinition("JwtSettings collection")]
    public class JwtSettingsCollection : ICollectionFixture<JwtSettings>
    {
    }
}
