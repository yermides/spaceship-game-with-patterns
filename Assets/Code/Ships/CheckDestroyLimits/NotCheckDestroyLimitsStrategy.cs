using UnityEngine;

namespace Code.Ships.CheckDestroyLimits
{
    public class NotCheckDestroyLimitsStrategy : ICheckDestroyLimits
    {
        public bool IsWithinLimits(Vector3 position)
        {
            return true;
        }
    }
}