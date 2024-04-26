
namespace Application.Responses;

public record ApplicationResponse(
    string StatusCode,
    bool IsSuccess, 
    string Message, 
    object? Data);