﻿using System.ComponentModel.DataAnnotations;

namespace netcoreAPI.Models
{
    public class CarCreate
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public int? FuelId { get; set; }

        [Required]
        public int? BrandId { get; set; }

        [Required]
        public int? ModelId { get; set; }
    }
}
