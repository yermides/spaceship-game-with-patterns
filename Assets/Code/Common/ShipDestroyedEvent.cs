using Code.Ships.Common;
using UnityEngine;

namespace Code.Common
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

            Debug.Log($"ShipDestroyedResponse created at frame {Time.frameCount}");
        }
    }
}