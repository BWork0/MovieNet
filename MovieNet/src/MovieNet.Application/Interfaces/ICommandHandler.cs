namespace MovieNet.Application.Interfaces
{
    public interface ICommandHandler<TCommand>
    {
        Task Handle(TCommand command);
    }
}
