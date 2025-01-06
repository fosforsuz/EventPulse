namespace EventPulse.Api.Models;

public class ResponseModel
{
    public bool IsSuccess { get; private set; }
    public string? Message { get; private set; }
    public object? Data { get; private set; }

    public static ResponseModel Success(object data)
    {
        return new ResponseModel
        {
            IsSuccess = true,
            Message = "Success",
            Data = data
        };
    }

    public static ResponseModel Success(object data, string message)
    {
        return new ResponseModel
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };
    }

    public static ResponseModel Error(string message)
    {
        return new ResponseModel
        {
            IsSuccess = false,
            Message = message
        };
    }
}