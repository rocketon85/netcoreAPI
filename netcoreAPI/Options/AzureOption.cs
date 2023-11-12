namespace netcoreAPI.Options
{
    public class AzureOption
    {
        public required string KeyVaultUrl { get; set; }
        public required FunctionsUrl FunctionsUrl { get; set; }
    }

    public class FunctionsUrl
    {
        public required string GetUserDetail { get; set; }
        public required string NewCar { get; set; } 
    }
}
