using Code.Ships;
using UnityEngine;

namespace Code.Input
{
    public class AIInputAdapter : IInputAdapter
    {
        private readonly ShipMediator _shipMediator;
        private readonly Transform _shipTransform;
        private readonly Camera _camera;
        private float _currentDirectionX = -1;

        public AIInputAdapter(ShipMediator shipMediator)
        {
            _shipMediator = shipMediator;
            _shipTransform = _shipMediator.transform;
            _camera = Camera.main;
        }
        
        public Vector3 GetDirection()
        {
            const float viewThreshold = 0.05f;
            
            var viewpoint = _camera.WorldToViewportPoint(_shipTransform.position);

            if (viewpoint.x < viewThreshold)
            {
                _currentDirectionX = -_shipTransform.right.x;
            }
            else if (viewpoint.x > 1.0f - viewThreshold)
            {
                _currentDirectionX = _shipTransform.right.x;
            }

            return new Vector3(_currentDirectionX, 0, -1);
        }

        public bool DidRequestToFire()
        {
            // Always tries to fire
            return true;
        }
    }
}