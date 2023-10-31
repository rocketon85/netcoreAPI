namespace netcoreAPI.Options
{
    public class EnviromentOption
    {
        public required string DefaultCulture { get; set; }
        public required string[] AvailableCulture { get; set; }
        public required string ResourcePath { get; set; }
    }
}
