using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Util
{
    public class EventQueueImpl : MonoBehaviour, IEventQueue
    {
        private readonly Dictionary<Type, dynamic> _eventsToDelegatesDictionary;
        private readonly Queue<dynamic> _eventsQueued;

        public EventQueueImpl()
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

        private void LateUpdate()
        {
            while (_eventsQueued.Count > 0)
            {
                ProcessOne();
            }
        }
        
        private void ProcessOne()
        {
            var queuedEvent = _eventsQueued.Dequeue();

            Type type = queuedEvent.GetType();
            
            if (!_eventsToDelegatesDictionary.ContainsKey(type)) return;

            // Debug.Log($"{ signal.GetType().ToString() } and {type.ToString()}");
            
            var callback = _eventsToDelegatesDictionary[type]; // (Action<T>)
            callback?.Invoke(queuedEvent);
        }
    }
}