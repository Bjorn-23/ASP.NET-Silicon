using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Infrastructure.Factories;

public class ResponseFactory
{
    public static ResponseResult Ok()
    {
        return new ResponseResult()
        {
            Message = "Success",
            StatusCode = StatusCode.OK
        };
    }

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
            ContentResult = obj,
            Message = message ?? "Success",
            StatusCode = StatusCode.OK
        };
    }

    public static ResponseResult Error(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, operation failed",
            StatusCode = StatusCode.ERROR
        };
    }

    public static ResponseResult NotFound(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, entity not found",
            StatusCode = StatusCode.NOT_FOUND
        };
    }

    public static ResponseResult Exists(string? message = null)
    {
        return new ResponseResult()
        {
            Message = message ?? "Error, entity already exists",
            StatusCode = StatusCode.EXISTS
        };
    }
}
