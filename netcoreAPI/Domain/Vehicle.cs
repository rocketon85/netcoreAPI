using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace netcoreAPI.Domain
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double MaxSpeed { get; set; }

        [ForeignKey("Fuel")]
        public int FuelId { get; set; }
        public Fuel Fuel { get; set; } 

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [ForeignKey("Model")]
        public int ModelId { get; set; }
        public Model Model { get; set; }
    }
}
