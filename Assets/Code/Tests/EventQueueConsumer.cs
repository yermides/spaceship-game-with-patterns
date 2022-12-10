using System;
using Code.Common;
using Code.Common.Events;
using Code.Ships.Common;
using UnityEngine;

namespace Code.Tests
{
    [Obsolete]
    public class EventQueueConsumer : MonoBehaviour
    {
        /*
        private void OnEnable()
        {
            EventQueue.Instance.Subscribe(EventId.ShipDestroyed, this);
        }

        private void OnDisable()
        {
            EventQueue.Instance.Unsubscribe(EventId.ShipDestroyed, this);
        }

        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.W))
            {
                EventQueue.Instance.EnqueueEvent(new ShipDestroyedEvent(Teams.Enemy, 10, 0));
            }
        }

        public void Process(EventArgsBase args)
        {
            Debug.Log("I am talking when processed");
        }
        */
    }
}
