namespace netcoreAPI.Domain
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
