using Code.Util;
using UnityEngine;

namespace Code.Common.Events
{
    public class ShipSpawnedEvent : IDispatchedEvent
    {
        public ShipSpawnedEvent()
        {
            Debug.Log($"ShipSpawnedEvent created at frame {Time.frameCount}");
        }
    }
}