namespace NewsPortalCMS.Shared.Responses;

public static class ApiResponseFactory
{
    public static ApiResponse<T> Success<T>(
        T data,
        string message = "Request completed successfully.",
        int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            StatusCode = statusCode,
            Data = data
        };
    }

    public static ApiResponse<T> Failure<T>(
        string message,
        List<string>? errors = null,
        int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            StatusCode = statusCode,
            Errors = errors ?? new List<string>()
        };
    }
}