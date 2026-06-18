using MediatR;

namespace Restaurant.Application.Messaging;

public interface IQuery<out TResponse>: IRequest<TResponse>
{
    
}