using Infrastructure.Utilities;

namespace Infrastructure.Factories;

public class ResponseFactory
{
    public static ResponseResult Ok(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Success",
            StatusCode = StatusCode.OK
        };
    }

    public static ResponseResult Ok(object obj, string? message = null)
    {
        return new ResponseResult()
        {
            Content = obj,
            Message = message ?? "Success",
            StatusCode = StatusCode.OK
        };
    }

    public static ResponseResult NoContent(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Success",
            StatusCode = StatusCode.NO_CONTENT
        };
    }

    public static ResponseResult Created(object obj, string? message = null)
    {
        return new ResponseResult()
        {
            Content = obj,
            Message = message ?? "Success",
            StatusCode = StatusCode.CREATED
        };
    }

    public static ResponseResult BadRequest(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, operation failed",
            StatusCode = StatusCode.BAD_REQUEST
        };
    }

    public static ResponseResult Unauthorized(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Access denied - please login",
            StatusCode = StatusCode.UNAUTHORIZED
        };
    }

    public static ResponseResult Forbidden(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Access denied - you don't have the right credentials",
            StatusCode = StatusCode.FORBIDDEN
        };
    }

    public static ResponseResult NotFound(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, not found",
            StatusCode = StatusCode.NOT_FOUND
        };
    }

    public static ResponseResult NotAllowed(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, method not allowed",
            StatusCode = StatusCode.NOT_FOUND
        };
    }

    public static ResponseResult Exists(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, already exists",
            StatusCode = StatusCode.EXISTS
        };
    }

    public static ResponseResult UnsupportedMedia(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, media not supported",
            StatusCode = StatusCode.UNSUPPORTED_MEDIA_TYPE
        };
    }

    public static ResponseResult InternalServerError(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, internal server error",
            StatusCode = StatusCode.INTERNAL_SERVER_ERROR
        };
    }

    public static ResponseResult NotImplemented(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, service not implemented",
            StatusCode = StatusCode.NOT_INPLEMENTED
        };
    }

    public static ResponseResult BadGateway(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, bad gateway",
            StatusCode = StatusCode.BAD_GATEWAY
        };
    }

    public static ResponseResult ServiceUnavailable(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, service unavailable",
            StatusCode = StatusCode.SERVICE_UNAVAILABLE
        };
    }
}
