using MovieNet.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MovieNet.Application.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Send<TCommand>(TCommand command)
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.Handle(command);
        }

        public async Task<TResult> Query<TQuery, TResult>(TQuery query)
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await handler.Handle(query);
        }
    }
}
