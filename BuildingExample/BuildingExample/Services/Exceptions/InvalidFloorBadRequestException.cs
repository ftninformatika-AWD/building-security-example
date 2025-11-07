namespace BuildingExample.Services.Exceptions
{
    public class InvalidFloorBadRequestException : BadRequestException
    {
        public InvalidFloorBadRequestException() : base("Minimal floor value cannot be " +
                    "greater than maximal floor value")
        {
        }
    }
}
