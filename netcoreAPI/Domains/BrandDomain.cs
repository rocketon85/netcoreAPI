using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netcoreAPI.Domains
{
    public class BrandDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<ModelDomain> Models { get; set; } = new List<ModelDomain>();
    }
}
