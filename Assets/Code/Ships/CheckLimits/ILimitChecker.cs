using UnityEngine;

namespace Code.Ships.CheckLimits
{
    public interface ILimitChecker
    {
        Vector3 ClampFinalPosition(Vector3 positionToClamp);
    }
}