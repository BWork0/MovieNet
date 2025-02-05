namespace MovieNet.Application.Interfaces
{
    public interface IMediator
    {
        Task Send<TCommand>(TCommand command);
        Task<TResult> Query<TQuery, TResult>(TQuery query);
    }
}
