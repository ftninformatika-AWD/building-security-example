namespace BuildingExample.Exceptions
{
    public class InvalidAreaBadRequestException : BadRequestException
    {
        public InvalidAreaBadRequestException() : base("Minimal area value cannot be " +
                    "greater than maximal area value")
        {
        }
    }
}
