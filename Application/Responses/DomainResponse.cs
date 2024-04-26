
namespace Application.Responses;

public record DomainResponse<T>
{
    public DomainResponse() { }

    public bool Success { get; init; }

    public string Message { get; init; } = string.Empty;

    public T? Data { get; init; }
}
