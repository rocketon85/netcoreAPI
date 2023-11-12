using System.ComponentModel.DataAnnotations.Schema;

namespace netcoreAPI.Identity
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Password { get; set; }

        [NotMapped]
        public string Detail { get; set; }
    }
}
