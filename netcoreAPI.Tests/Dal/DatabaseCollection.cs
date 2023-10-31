using netcoreAPI.Context;

namespace netcoreAPI.Tests.Dal
{
    [CollectionDefinition("Database collection")]
    public class DbSettingsCollection : ICollectionFixture<TestDbContext>
    {
    }
}
