using UnityEngine;

namespace Code.Common.Events
{
    public class GameOverEvent : EventArgsBase
    {
        public GameOverEvent() : base(EventId.GameOver)
        {
            Debug.Log($"GameOverEvent created at frame {Time.frameCount}");
        }
    }
}