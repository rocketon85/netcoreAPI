using netcoreAPI.Context;

namespace netcoreAPI.Tests.Collections
{
    [CollectionDefinition("Enviroment collection")]
    public class EnviromentCollection : ICollectionFixture<StartUp>
    {
    }
}
