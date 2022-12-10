using Code.Util;
using UnityEngine;

namespace Code.Common.Events
{
    public class VictoryEvent : IDispatchedEvent
    {
        public VictoryEvent()
        {
            Debug.Log($"VictoryEvent created at frame {Time.frameCount}");
        }
    }
}