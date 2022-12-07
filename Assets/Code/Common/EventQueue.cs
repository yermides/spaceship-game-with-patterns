using System;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;

namespace Code.Common
{
    [DefaultExecutionOrder(-2)]
    public class EventQueue : MonoBehaviour
    {
        private static EventQueue _instance;
        public static EventQueue Instance => _instance;

        private Queue<EventArgsBase> _eventsBeingProcessed;
        private Queue<EventArgsBase> _eventsToProcessNext;

        private Dictionary<EventId, List<IEventObserver>> _eventObservers;

        private void Awake()
        {
            _instance = this;
            _eventsBeingProcessed = new Queue<EventArgsBase>();
            _eventsToProcessNext = new Queue<EventArgsBase>();
            _eventObservers = new Dictionary<EventId, List<IEventObserver>>();
        }

        public void EnqueueEvent(EventArgsBase eventArgsBase)
        {
            _eventsToProcessNext.Enqueue(eventArgsBase);
        }

        public void Subscribe(EventId id, IEventObserver observer)
        {
            if (!_eventObservers.TryGetValue(id, out var observerList))
            {
                observerList = new List<IEventObserver>();
            }
            
            observerList.Add(observer);
            _eventObservers[id] = observerList;
        }

        public void Unsubscribe(EventId id, IEventObserver observer)
        {
            if (!_eventObservers.TryGetValue(id, out var observerList)) return;
            
            observerList.Remove(observer);
        }

        private void LateUpdate()
        {
            ProcessEvents();
        }

        private void ProcessEvents()
        {
            (_eventsBeingProcessed, _eventsToProcessNext) = (_eventsToProcessNext, _eventsBeingProcessed);

            foreach (var eventArgsBase in _eventsBeingProcessed)
            {
                ProcessEvent(eventArgsBase);
            }
            
            _eventsBeingProcessed.Clear();

            // if events dispatch new events, be able to process them in the same frame
            // if (_eventsToProcessNext.Count > 0)
            // {
            //     ProcessEvents();
            // }
        }

        private void ProcessEvent(EventArgsBase eventArgsBase)
        {
            if (!_eventObservers.TryGetValue(eventArgsBase.EventId, out var eventObserverList)) return;

            foreach (var eventObserver in eventObserverList)
            {
                eventObserver.Process(eventArgsBase);
            }
        }
    }
}