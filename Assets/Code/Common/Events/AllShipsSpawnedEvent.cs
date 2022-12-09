using UnityEngine;

namespace Code.Common.Events
{
    public class AllShipsSpawnedEvent : EventArgsBase
    {
        public AllShipsSpawnedEvent() : base(EventId.AllShipsSpawned)
        {
            Debug.Log($"AllShipsSpawnedEvent created at frame {Time.frameCount}");
        }
    }
}