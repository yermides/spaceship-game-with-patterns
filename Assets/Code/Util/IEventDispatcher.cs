using System;
using Code.Tests;

namespace Code.Util
{
    public interface IEventDispatcher
    {
        void Subscribe<T>(Action<T> callback) where T : IDispatchedEvent;
        void Unsubscribe<T>(Action<T> callback) where T : IDispatchedEvent;
        void Dispatch<T>(T signal) where T : IDispatchedEvent;
    }
}