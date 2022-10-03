using UnityEngine;

namespace Gameplay
{
    public interface IShipState
    {
        public void PerformAction(object context);
    }
}