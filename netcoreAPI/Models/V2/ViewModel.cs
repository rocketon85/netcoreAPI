namespace netcoreAPI.Models.V2
{
    public record CarViewModel(int Id, string Name, bool HasGraffiti, string FuelName, string BrandName, string ModelName);

    public record CarCreateModel {
        public required string Name { get; set; }
        public required int FuelId { get; set; }
        public required int BrandId { get; set; }
        public required int ModelId { get; set; }
        public required int Doors { get; set; }
    } 
}
