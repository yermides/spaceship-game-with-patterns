using System;
using System.Collections.Generic;
using Code.Common;
using Code.Tests;
using UnityEngine;

namespace Code.Util
{
    // Second (and better) way of dispatching events

    public class EventDispatcherImpl : IEventDispatcher
    {
        private readonly Dictionary<Type, dynamic> _eventsToDelegatesDictionary;
        private readonly Queue<dynamic> _eventsQueued;

        public EventDispatcherImpl()
        {
            _eventsToDelegatesDictionary = new Dictionary<Type, dynamic>();
            _eventsQueued = new Queue<dynamic>();
        }

        public void Subscribe<T>(Action<T> callback) where T : IDispatchedEvent
        {
            var type = typeof(T);
            
            if (!_eventsToDelegatesDictionary.ContainsKey(type)) _eventsToDelegatesDictionary.Add(type, null);

            _eventsToDelegatesDictionary[type] += callback;
        }

        public void Unsubscribe<T>(Action<T> callback) where T : IDispatchedEvent
        {
            var type = typeof(T);

            if (_eventsToDelegatesDictionary.ContainsKey(type))
            {
                _eventsToDelegatesDictionary[type] -= callback;
            }
        }

        public void Dispatch<T>(T signal) where T : IDispatchedEvent
        {
            Type type = typeof(T);
            
            if (!_eventsToDelegatesDictionary.ContainsKey(type)) return;

            // Debug.Log($"{ signal.GetType().ToString() } and {type.ToString()}");
            
            var callback = _eventsToDelegatesDictionary[type]; // (Action<T>)
            callback?.Invoke(signal);
        }
    }
}

#region OldImplementation

/*
 using System;
using System.Collections.Generic;
using Code.Common;
using Code.Tests;
using UnityEngine;

namespace Code.Util
{
    // Second (and better) way of dispatching events

    public class EventDispatcherImpl : IEventDispatcher
    {
        private readonly Dictionary<Type, dynamic> _eventsToDelegatesDictionary;
        private readonly Queue<dynamic> _eventsQueued;

        public EventDispatcherImpl()
        {
            _eventsToDelegatesDictionary = new Dictionary<Type, dynamic>();
            _eventsQueued = new Queue<dynamic>();
        }

        public void Subscribe<T>(Action<T> callback) where T : IDispatchedEvent
        {
            var type = typeof(T);
            
            if (!_eventsToDelegatesDictionary.ContainsKey(type)) _eventsToDelegatesDictionary.Add(type, null);

            _eventsToDelegatesDictionary[type] += callback;
        }

        public void Unsubscribe<T>(Action<T> callback) where T : IDispatchedEvent
        {
            var type = typeof(T);

            if (_eventsToDelegatesDictionary.ContainsKey(type))
            {
                _eventsToDelegatesDictionary[type] -= callback;
            }
        }

        public void Enqueue<T>(T signal) where T : IDispatchedEvent
        {
            _eventsQueued.Enqueue(signal);
        }

        // Process all enqueued events and dequeues them

        public void ProcessOneQueued()
        {
            if (_eventsQueued.Count > 0) return;

            DequeueAndProcess();
        }

        public void ProcessAllQueued()
        {
            while (_eventsQueued.Count > 0)
            {
                DequeueAndProcess();
            }
        }

        private void DequeueAndProcess()
        {
            // Process if exists
            var queuedEvent = _eventsQueued.Dequeue();
            Dispatch(queuedEvent.GetType(), queuedEvent);
        }

        public void Dispatch<T>(T signal) where T : IDispatchedEvent
        {
            Dispatch(typeof(T), signal);
        }

        private void Dispatch(Type type, dynamic signal)
        {
            if (!_eventsToDelegatesDictionary.ContainsKey(type)) return;

            // Debug.Log($"{ signal.GetType().ToString() } and {type.ToString()}");
            
            var callback = _eventsToDelegatesDictionary[type]; // (Action<T>)
            callback?.Invoke(signal);
        }
    }
}
 */
#endregion