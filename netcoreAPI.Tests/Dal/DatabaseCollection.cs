using netcoreAPI.Dal;

namespace netcoreAPI.Tests.Dal
{
    [CollectionDefinition("Database collection")]
    public class JwtSettingsCollection : ICollectionFixture<TestDbContext>
    {
    }
}
