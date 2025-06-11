using System.ComponentModel.DataAnnotations;

namespace BuildingExample.DTOs
{
    public class ApartmentSearchDTO
    {
        [Required]
        [Range(0, int.MaxValue)]
        public double AreaFrom { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double AreaTo { get; set; }
    }
}
