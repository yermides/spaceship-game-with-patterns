using Code.Ships;
using UnityEngine;

namespace Code.Input
{
    public class AIInputAdapter : IInputAdapter
    {
        private readonly Ship _ship;
        private readonly Transform _shipTransform;
        private readonly Camera _camera;
        private float _currentDirectionX = -1;

        public AIInputAdapter(Ship ship)
        {
            _ship = ship;
            _shipTransform = _ship.transform;
            _camera = Camera.main;
        }
        
        public Vector3 GetDirection()
        {
            const float viewThreshold = 0.05f;
            
            var viewpoint = _camera.WorldToViewportPoint(_shipTransform.position);

            if (viewpoint.x < viewThreshold)
            {
                _currentDirectionX = 1;
            }
            else if (viewpoint.x > 1.0f - viewThreshold)
            {
                _currentDirectionX = -1;
            }

            return new Vector3(_currentDirectionX, 0, 0);
        }
    }
}