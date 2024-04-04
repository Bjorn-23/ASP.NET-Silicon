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
            Message = message ?? "Error, Operation Failed",
            StatusCode = StatusCode.BAD_REQUEST
        };
    }

    public static ResponseResult Unauthorized(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Access denied - Please Login",
            StatusCode = StatusCode.UNAUTHORIZED
        };
    }

    public static ResponseResult Forbidden(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Forbidden",
            StatusCode = StatusCode.FORBIDDEN
        };
    }

    public static ResponseResult NotFound(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, Not Found",
            StatusCode = StatusCode.NOT_FOUND
        };
    }

    public static ResponseResult NotAllowed(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, Method Not Allowed",
            StatusCode = StatusCode.NOT_FOUND
        };
    }

    public static ResponseResult Exists(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, Already Exists",
            StatusCode = StatusCode.EXISTS
        };
    }

    public static ResponseResult UnsupportedMedia(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, Media Not Supported",
            StatusCode = StatusCode.UNSUPPORTED_MEDIA_TYPE
        };
    }

    public static ResponseResult InternalServerError(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Internal Server Error - please try again later",
            StatusCode = StatusCode.INTERNAL_SERVER_ERROR
        };
    }

    public static ResponseResult NotImplemented(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, Service Not Implemented",
            StatusCode = StatusCode.NOT_INPLEMENTED
        };
    }

    public static ResponseResult BadGateway(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, Bad Gateway",
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
