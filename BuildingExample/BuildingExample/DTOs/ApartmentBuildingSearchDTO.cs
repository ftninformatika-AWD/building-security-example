using System.ComponentModel.DataAnnotations;

namespace BuildingExample.DTOs
{
    public class ApartmentBuildingSearchDTO
    {
        [Required]
        [Range(0, int.MaxValue)]
        public double FloorFrom { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double FloorTo { get; set; }

        [Required]
        public int BuildingId { get; set; }
    }
}
