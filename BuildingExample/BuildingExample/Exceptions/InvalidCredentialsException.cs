namespace BuildingExample.Exceptions
{
    public class InvalidCredentialsException : UnauthorizedException
    {
        public InvalidCredentialsException(string message) : 
            base($"Invalid credentials were provided. Following error occured: { message }.")
        {
        }
    }
}
