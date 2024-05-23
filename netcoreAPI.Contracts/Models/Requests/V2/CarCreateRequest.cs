namespace netcoreAPI.Contracts.Models.Requests.V2
{
    public record  CarCreateRequest
    {
        public required string Name { get; set; }
        public required int FuelId { get; set; }
        public required int BrandId { get; set; }
        public required int ModelId { get; set; }
        public required int Doors { get; set; }
    }
}
