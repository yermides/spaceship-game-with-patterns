namespace Code.Util
{
    public interface IEventReceiver<in T> where T : IDispatchedEvent
    {
        void OnEvent(T signal);
    }
}