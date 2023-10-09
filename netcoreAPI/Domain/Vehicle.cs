namespace netcoreAPI.Domain
{
    public class Vehicle
    {
        public int Id { get; set; }

        public int FuelId { get; set; }
        public Fuel Fuel { get; set; }
        public double MaxSpeed { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }
    }
}
