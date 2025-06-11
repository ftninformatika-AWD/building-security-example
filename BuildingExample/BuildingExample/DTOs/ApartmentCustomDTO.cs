namespace BuildingExample.DTOs
{
    public class ApartmentCustomDTO
    {
        public int Id { get; set; }
        public required string Location { get; set; }
        public double Area { get; set; }
        public required string Building { get; set; }
    }
}
