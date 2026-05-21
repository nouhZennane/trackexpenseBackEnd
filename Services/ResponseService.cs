using Microsoft.AspNetCore.Mvc;

namespace TrackExences.Services;

public static class ResponseService
{
    public static object Handle(HttpRequest httpRequest, bool isDev)
    {
        string method = httpRequest.Method;
        string path = httpRequest.Path;
        object response ;
        if (isDev)
        {
            response = new
            {
                message = "This Http Method Not Allowed",
                method,
                statusCode = 404,
                path = $"{path}"
            };
        }
        else
        {
            response = new
            {
                message = $"Invalid route {path}",
            };
        }
        return response;
    }
}