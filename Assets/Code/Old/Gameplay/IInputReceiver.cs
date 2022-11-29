using UnityEngine;

namespace Gameplay
{
    public interface IInputReceiver
    {
        public Vector2 GetDirection();
        public bool GetRequestForFiring();
    }
}