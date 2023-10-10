namespace netcoreAPI.Models
{
    public record CarViewModel(int Id, string FuelName, string BrandName, string ModelName);

    public record AuthRespModel(int UserId, string Token);
}
