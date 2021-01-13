namespace UserService.Domain.Common
{
    public interface IEventDispatcher
    {
        void PublishEvent<TEvent>(TEvent @event);
    }
}