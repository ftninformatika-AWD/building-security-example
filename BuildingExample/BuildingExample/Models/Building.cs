using System.ComponentModel.DataAnnotations;

namespace BuildingExample.Models
{
    public class Building
    {
        public int Id { get; set; }

        [Required]
        public required string Address { get; set; }

        [Range(0, int.MaxValue)]
        public int Floors { get; set; }

        public bool HasElevator { get; set; }

        [Range(0, int.MaxValue)]
        public int YearOfConstruction { get; set; }
    }
}
