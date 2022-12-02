using UnityEngine;

namespace Code.Ships.CheckLimits
{
    public class InitialPositionLimitChecker : ILimitChecker
    {
        private readonly Transform _transform;
        private readonly Vector3 _initialPosition;
        private readonly float _maxDistance;

        public InitialPositionLimitChecker(Transform transform, float maxDistance)
        {
            _transform = transform;
            _initialPosition = _transform.position;
            _maxDistance = maxDistance;
        }
        
        public void ClampFinalPosition()
        {
            var currentPosition = _transform.position;
            var finalPosition = currentPosition;
            var distance = Mathf.Abs(currentPosition.x - _initialPosition.x);

            if (distance <= _maxDistance) return;

            // Check whether is left or right bound
            var distanceToAdd = currentPosition.x > _initialPosition.x ? _maxDistance : -_maxDistance;

            finalPosition.x = _initialPosition.x + distanceToAdd;
            _transform.position = finalPosition;
        }
    }
}