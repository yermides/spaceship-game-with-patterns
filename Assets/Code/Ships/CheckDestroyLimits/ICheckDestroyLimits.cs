using UnityEngine;

namespace Code.Ships.CheckDestroyLimits
{
    public interface ICheckDestroyLimits
    {
        bool IsWithinLimits(Vector3 position);
    }
}