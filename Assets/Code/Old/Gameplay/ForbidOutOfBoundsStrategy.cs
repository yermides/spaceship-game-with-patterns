using UnityEngine;

namespace Gameplay
{
    public class ForbidOutOfBoundsStrategy : IMovementConstrainer
    {
        private float _boundaryMargin;
        private Camera _camera;

        public ForbidOutOfBoundsStrategy(float boundaryMargin = 0.03f)
        {
            _boundaryMargin = boundaryMargin;
            _camera = Camera.main;
        }

        public void CheckAndCorrectPosition(Transform transform)
        {
            // Clamp to make sure we don't go off the screen
            Vector3 viewportPoint = _camera.WorldToViewportPoint(transform.position);
            viewportPoint.x = Mathf.Clamp(viewportPoint.x, _boundaryMargin, 1.0f - _boundaryMargin);
            viewportPoint.y = Mathf.Clamp(viewportPoint.y, _boundaryMargin, 1.0f - _boundaryMargin);

            Vector3 adjustedPosition = _camera.ViewportToWorldPoint(viewportPoint);
            adjustedPosition.y = 0;

            transform.position = adjustedPosition;
        }
    }
}