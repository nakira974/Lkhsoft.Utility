namespace Lkhsoft.Utility.WebServices;

/// <inheritdoc />
public class HttpBaseRequestException : Exception
{
    public HttpBaseRequestException(string? message) : base(message)
    {
    }

    public HttpBaseRequestException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}