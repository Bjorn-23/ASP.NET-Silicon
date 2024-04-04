namespace Infrastructure.Utilities;
public enum StatusCode
{
    OK = 200,
    CREATED = 201,
    NO_CONTENT = 204,
    BAD_REQUEST = 400,
    UNAUTHORIZED = 401,
    FORBIDDEN = 403,
    NOT_FOUND = 404,
    METHOD_NOT_ALLOWED = 405,
    EXISTS = 409,
    UNSUPPORTED_MEDIA_TYPE = 415,
    INTERNAL_SERVER_ERROR = 500,
    NOT_INPLEMENTED = 501,
    BAD_GATEWAY = 502,
    SERVICE_UNAVAILABLE = 502

}

public class ResponseResult
{
    public StatusCode? StatusCode { get; set; }
    public object? Content { get; set; }
    public string? Message { get; set; }
}
