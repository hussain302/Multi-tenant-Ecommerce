using MediatR;

namespace Application.Abstractions.MediatR;
internal interface ICommand<TResult> : IRequest<TResult>
{
}
