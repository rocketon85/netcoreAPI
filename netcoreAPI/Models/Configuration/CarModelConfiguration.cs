using Asp.Versioning;
using Asp.Versioning.OData;
using Microsoft.OData.ModelBuilder;
using netcoreAPI.Domain;
using System;

namespace netcoreAPI.Models.Configuration
{
    public class CarModelConfiguration : IModelConfiguration
    {
        private static void ConfigureV1(ODataModelBuilder builder)
        {
            var car = ConfigureCurrent(builder);
            car.Ignore(p => p.Doors);
        }

        private static void ConfigureV2(ODataModelBuilder builder) => ConfigureCurrent(builder).Ignore(p => p.HasGraffiti);

        private static EntityTypeConfiguration<Car> ConfigureCurrent(ODataModelBuilder builder)
        {
            var car = builder.EntitySet<Car>("CarModel").EntityType;

            car.HasKey(p => p.Id);

            return car;
        }

        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
        {
            switch (apiVersion.MajorVersion)
            {
                case 1:
                    ConfigureV1(builder);
                    break;
                case 2:
                    ConfigureV2(builder);
                    break;
                default:
                    ConfigureCurrent(builder);
                    break;
            }
        }
    }
}