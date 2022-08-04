using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace Gameplay
{
    public interface IMovementConstrainer
    {
        public void CheckAndCorrectPosition(Transform transform);
    }
}