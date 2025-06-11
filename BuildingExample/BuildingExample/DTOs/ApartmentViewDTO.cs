namespace BuildingExample.DTOs
{
    public class ApartmentViewDTO
    {
        public int Id { get; set; }
        public double Area { get; set; }
        public int NumberOfRooms { get; set; }
        public required string BuildingAddress { get; set; }
    }
}
