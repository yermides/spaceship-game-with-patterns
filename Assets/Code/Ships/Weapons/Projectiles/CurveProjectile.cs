using System.Collections;
using UnityEngine;

namespace Code.Ships.Weapons.Projectiles
{
    public class CurveProjectile : ProjectileBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private AnimationCurve horizontalPositionCurve;
        
        private float _currentTime;
        private Vector3 _currentPosition;

        protected override void DoStart()
        {
            myTransform = transform;
            _currentTime = 0;
            _currentPosition = myTransform.position;
        }

        protected override void DoMove()
        {
            // Forward movement
            _currentPosition += transform.forward * (speed * Time.fixedDeltaTime);
            // Lateral movement
            var horizontalPosition = myTransform.right * horizontalPositionCurve.Evaluate(_currentTime);
            rigidbodyReference.MovePosition(_currentPosition + horizontalPosition);
            
            _currentTime += Time.fixedDeltaTime;
        }

        protected override void DoDestroy()
        {
        }
    }
}