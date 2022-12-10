using Code.Util;
using UnityEngine;

namespace Code.Common.Events
{
    public class AllShipsSpawnedEvent : IDispatchedEvent
    {
        public AllShipsSpawnedEvent()
        {
            Debug.Log($"AllShipsSpawnedEvent created at frame {Time.frameCount}");
        }
    }
}