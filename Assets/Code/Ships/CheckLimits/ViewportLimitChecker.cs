using UnityEngine;

namespace Code.Ships.CheckLimits
{
    public class ViewportLimitChecker : ILimitChecker
    {
        private readonly Transform _transform;
        private readonly Camera _camera;

        public ViewportLimitChecker(Transform transform)
        {
            _transform = transform;
            _camera = Camera.main;
        }
        
        public void ClampFinalPosition()
        {
            const float margin = 0.03f;
            
            var viewpoint = _camera.WorldToViewportPoint(_transform.position);
            viewpoint.x = Mathf.Clamp(viewpoint.x, margin, 1.0f - margin);
            viewpoint.y = Mathf.Clamp(viewpoint.y, margin, 1.0f - margin);
            _transform.position = _camera.ViewportToWorldPoint(viewpoint);
        }
    }
}