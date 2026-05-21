namespace TrackExences.Exceptions;

public class ValidationException: Exception
{
    public  ValidationException(string message, int statusCode): base(message)
    {
    }
}