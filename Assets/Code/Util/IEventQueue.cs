using System;

namespace Code.Util
{
    public interface IEventQueue
    {
        void Subscribe<T>(Action<T> callback) where T : IDispatchedEvent;
        void Unsubscribe<T>(Action<T> callback) where T : IDispatchedEvent;
        void Enqueue<T>(T signal) where T : IDispatchedEvent;
    }
}