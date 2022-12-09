using UnityEngine;

namespace Code.Common.Events
{
    public class VictoryEvent : EventArgsBase
    {
        public VictoryEvent() : base(EventId.Victory)
        {
            Debug.Log($"VictoryEvent created at frame {Time.frameCount}");
        }
    }
}