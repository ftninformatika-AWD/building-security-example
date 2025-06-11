using System.ComponentModel.DataAnnotations;

namespace BuildingExample.DTOs
{
    public class ApartmentUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public required string ApartmentNumber { get; set; }
        [Range(0, int.MaxValue)]
        public int Floor { get; set; }
        [Range(0, int.MaxValue)]
        public double Area { get; set; }  // in square meters
        [Range(1, int.MaxValue)]
        public int NumberOfRooms { get; set; }
        public int BuildingId { get; set; }
    }
}
