namespace netcoreAPI.Contracts.Models.Requests.V1
{
    public record CarCreateRequest
    {
        public required int FuelId { get; set; }
        public required int BrandId { get; set; }
        public required int ModelId { get; set; }
    }
}
