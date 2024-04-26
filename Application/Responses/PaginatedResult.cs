namespace Application.Responses;

public record PaginatedResult<T>
{
    public T? Items { get; set; }
    public int ItemCount { get; set; }
}