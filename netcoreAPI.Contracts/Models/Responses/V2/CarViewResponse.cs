namespace netcoreAPI.Contracts.Models.Responses.V2
{
    public record CarViewResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required bool HasGraffiti { get; set; }
        public required string FuelName { get; set; }
        public required string BrandName { get; set; }
        public required string ModelName { get; set; }
    }
}
