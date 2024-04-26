using MediatR;

namespace Application.Abstractions.MediatR;
public interface IQuery<TResult> : IRequest<TResult>
{
}