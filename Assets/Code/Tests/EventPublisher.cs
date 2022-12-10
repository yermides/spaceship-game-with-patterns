using Code.Common.Events;
using Code.Util;
using UnityEngine;

namespace Code.Tests
{
    public class EventPublisher : MonoBehaviour
    {
        [SerializeField] private KeyCode keyCode;
        [SerializeField] private KeyCode keyCode2;
        [SerializeField] private KeyCode keyCode3;
        
        private void Update()
        {
            // var dispatcher = ServiceLocator.Instance.GetService<IEventDispatcher>();
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            
            if (UnityEngine.Input.GetKeyDown(keyCode))
            {
                // dispatcher.Dispatch(new GenericEvent());
                // dispatcher.Dispatch(new GameObjectDestroyedEvent(GetInstanceID()));
            }
            
            if (UnityEngine.Input.GetKeyDown(keyCode2))
            {
                Debug.Log(Time.frameCount);
                eventQueue.Enqueue(new SampleEvent());
            }
        }
    }
}