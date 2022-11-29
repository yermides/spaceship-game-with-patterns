using UnityEngine;

namespace Gameplay
{
    public class AIInputAdapter : IInputReceiver
    {
        private Ship _ship;
        private ShipFiringMediator _mediator;
        private Camera _camera;
        private Vector2 _currentDirection;

        public AIInputAdapter(Ship ship)
        {
            _ship = ship;
            _camera = Camera.main;
            _currentDirection = Vector2.right;
            _mediator = _ship.GetComponent<ShipFiringMediator>();
            // _currentDirection = Vector2.down;
        }

        public Vector2 GetDirection()
        {
            Vector3 viewportPoint = _camera.WorldToViewportPoint(_ship.transform.position);
            
            // Simple AI movement strategy
            // Change direction if we hit left or right boundaries for example
            if (viewportPoint.x < 0.05f)
            {
                _currentDirection = Vector2.left;
            } 
            else if (viewportPoint.x > 0.95f)
            {
                _currentDirection = Vector2.right;
            }

            if (viewportPoint.y < 0.0f || viewportPoint.y > 1.0f)
            {
                Object.Destroy(_ship.gameObject);
            }

            return _currentDirection;
        }

        public bool GetRequestForFiring()
        {
            return _ship.CanFire; // Fire whenever you are able
            
            // For now, the AI doesn't shoot
            // return false;
        }
    }
}