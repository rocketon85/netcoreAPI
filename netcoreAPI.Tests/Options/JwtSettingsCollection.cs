﻿using netcoreAPI.Options;

namespace netcoreAPI.Tests.Options
{
    [CollectionDefinition("JwtSettings collection")]
    public class JwtSettingsCollection : ICollectionFixture<JwtOption>
    {
    }
}
