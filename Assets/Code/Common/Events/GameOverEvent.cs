using Code.Util;
using UnityEngine;

namespace Code.Common.Events
{
    public class GameOverEvent : IDispatchedEvent
    {
        public GameOverEvent()
        {
            Debug.Log($"GameOverEvent created at frame {Time.frameCount}");
        }
    }
}