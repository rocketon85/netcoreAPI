using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netcoreAPI.Domains
{
    public class ModelDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public BrandDomain Brand { get; set; }
    }
}
