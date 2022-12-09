using UnityEngine;

namespace Code.Ships.CheckDestroyLimits
{
    public class CheckBottomLimitsStrategy : ICheckDestroyLimits
    {
        private readonly Camera _camera;

        public CheckBottomLimitsStrategy()
        {
            _camera = Camera.main;
        }
        
        public bool IsWithinLimits(Vector3 position)
        {
            var viewpoint = _camera.WorldToViewportPoint(position);
            return viewpoint.y > 0;
        }
    }
}