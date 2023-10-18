namespace netcoreAPI.Models.V1
{
    public record CarViewModel(int Id, string Name, string FuelName, string BrandName, string ModelName);

    public record CarCreateModel
    {
        public required int FuelId { get; set; }
        public required int BrandId { get; set; }
        public required int ModelId { get; set; }
    }
}
