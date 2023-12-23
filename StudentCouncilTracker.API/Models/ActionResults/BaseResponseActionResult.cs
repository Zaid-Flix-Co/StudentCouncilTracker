using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace StudentCouncilTracker.API.Models.ActionResults;

public sealed class BaseResponseActionResult<TValue> : IConvertToActionResult
{
    public ActionResult Result { get; } = null!;

    public BaseResponse<TValue> Value { get; } = null!;

    public BaseResponseActionResult(TValue value)
    {
        if (typeof(IActionResult).IsAssignableFrom(typeof(BaseResponse<TValue>)))
        {
            var error = $"{typeof(BaseResponse<TValue>)}: Value is assignable to IActionResult. Convert unavailable.";
            throw new ArgumentException(error);
        }

        Value = new BaseResponse<TValue>(value);
    }

    public BaseResponseActionResult(ActionResult result)
    {
        if (typeof(IActionResult).IsAssignableFrom(typeof(BaseResponse<TValue>)))
        {
            var error = $"{typeof(BaseResponse<TValue>)}: Value is assignable to IActionResult. Convert unavailable.";
            throw new ArgumentException(error);
        }

        Result = result ?? throw new ArgumentNullException(nameof(result));
    }

    public static implicit operator BaseResponseActionResult<TValue>(TValue value)
    {
        return new BaseResponseActionResult<TValue>(value);
    }

    public static implicit operator BaseResponseActionResult<TValue>(ActionResult result)
    {
        return new BaseResponseActionResult<TValue>(result);
    }

    IActionResult IConvertToActionResult.Convert()
    {
        return Result ?? new ObjectResult(Value)
        {
            DeclaredType = typeof(BaseResponse<TValue>),
        };
    }
}