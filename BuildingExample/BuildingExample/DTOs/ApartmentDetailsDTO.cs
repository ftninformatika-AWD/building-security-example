
namespace BuildingExample.DTOs
{
    public class ApartmentDetailsDTO
    {
        public int Id { get; set; }
        public required string ApartmentNumber { get; set; }
        public int Floor { get; set; }
        public double Area { get; set; }
        public int NumberOfRooms { get; set; }
        public int BuildingId { get; set; }
        public required string BuildingAddress { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ApartmentDetailsDTO dTO &&
                   Id == dTO.Id &&
                   ApartmentNumber == dTO.ApartmentNumber &&
                   Floor == dTO.Floor &&
                   Area == dTO.Area &&
                   NumberOfRooms == dTO.NumberOfRooms &&
                   BuildingId == dTO.BuildingId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ApartmentNumber, Floor, Area, NumberOfRooms, BuildingId, BuildingAddress);
        }
    }
}
