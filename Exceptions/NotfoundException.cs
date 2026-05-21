namespace TrackExences.Exceptions;

public class NotfoundException: Exception
{

    public NotfoundException(string message, int statusCode) : base(message)
    {
    
    }
}