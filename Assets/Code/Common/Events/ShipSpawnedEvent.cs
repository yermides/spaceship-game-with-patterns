using UnityEngine;

namespace Code.Common.Events
{
    public class ShipSpawnedEvent : EventArgsBase
    {
        public ShipSpawnedEvent() : base(EventId.ShipSpawned)
        {
            Debug.Log($"ShipSpawnedEvent created at frame {Time.frameCount}");
        }
    }
}