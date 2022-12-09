using Code.Ships.Common;
using UnityEngine;

namespace Code.Common.Events
{
    public class ShipDestroyedEvent : EventArgsBase
    {
        public readonly Teams team;
        public readonly int scoreToAdd;
        public readonly int instanceId;

        public ShipDestroyedEvent(Teams team, int scoreToAdd, int instanceId) : base(EventId.ShipDestroyed)
        {
            this.team = team;
            this.scoreToAdd = scoreToAdd;
            this.instanceId = instanceId;

            Debug.Log($"ShipDestroyedEvent created at frame {Time.frameCount}");
        }
    }
}