namespace StudentCouncilTracker.API.Models;

public class BaseResponse<T>
{
    public bool Success => (Error ?? "") == string.Empty;

    public string Error { get; set; } = null!;

    public T Result { get; set; } = default!;

    public BaseResponse()
    {

    }

    public BaseResponse(T data)
    {
        Result = data;
    }
}