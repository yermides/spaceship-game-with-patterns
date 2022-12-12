using System;
using Code.Common.Events;
using Code.Util;
using UnityEngine;

namespace Code.Tests
{
    public class EventReceiver : MonoBehaviour
    {
        private void OnEnable()
        {
            // var dispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            // dispatcher.Subscribe<GenericEvent>(OnGenericEvent);
            
            var queue = ServiceLocator.Instance.GetService<IEventQueue>();
            // queue.Subscribe<SampleEvent>(OnGenericEvent);
        }

        private void OnDisable()
        {
            // var dispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            // dispatcher.Unsubscribe<GenericEvent>(OnGenericEvent);
            
            var queue = ServiceLocator.Instance.GetService<IEventQueue>();
            // queue.Unsubscribe<SampleEvent>(OnGenericEvent);
        }

        // private void OnGenericEvent(SampleEvent signal)
        // {
        //     Debug.Log($"I am answering to Generic event (once again) at frame {Time.frameCount}");
        // }
    }
}