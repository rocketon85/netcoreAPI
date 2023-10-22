namespace netcoreAPI.Domain
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public int FuelId { get; set; }
        public Fuel Fuel { get; set; } = new Fuel();
        public double MaxSpeed { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; } = new Brand();

        public int ModelId { get; set; }
        public Model Model { get; set; } = new Model();
    }
}
