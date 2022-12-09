using UnityEngine;

namespace Code.Ships.CheckLimits
{
    public class ViewportLimitChecker : ILimitChecker
    {
        private readonly Transform _transform;
        private readonly Camera _camera;

        public ViewportLimitChecker(Camera camera = null)
        {
            _camera = (camera != null) ? camera : Camera.main;
        }
        
        public Vector3 ClampFinalPosition(Vector3 positionToClamp)
        {
            const float margin = 0.03f;
            
            var viewpoint = _camera.WorldToViewportPoint(positionToClamp);
            viewpoint.x = Mathf.Clamp(viewpoint.x, margin, 1.0f - margin);
            // viewpoint.y = Mathf.Clamp(viewpoint.y, margin, 1.0f - margin);
            viewpoint.y = Mathf.Clamp(viewpoint.y, 0.0f, 1.03f);
            return _camera.ViewportToWorldPoint(viewpoint);
            // _transform.position = _camera.ViewportToWorldPoint(viewpoint);
        }
    }
}