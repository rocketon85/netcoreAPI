namespace netcoreAPI.Models
{
    public record CarViewModel(int Id, string Name, string FuelName, string BrandName, string ModelName);

    public record CarCreateModel(int FuelId, int BrandId, int ModelId);

    public record AuthRespModel(int UserId, string Token);

    public record ApiInfoModel(string Author, string Version);
}
