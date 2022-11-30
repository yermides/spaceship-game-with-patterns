using System.Collections;
using UnityEngine;

namespace Code.Ships.Weapons.Projectiles
{
    public class SinusoidalProjectile : ProjectileBehaviour
    {
        [SerializeField] private float amplitude;
        [SerializeField] private float frequency;
        [SerializeField] private float speed;
        
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
            var horizontalPosition = myTransform.right * (amplitude * Mathf.Sin(_currentTime * frequency));
            rigidbodyReference.MovePosition(_currentPosition + horizontalPosition);
            
            _currentTime += Time.fixedDeltaTime;
        }

        protected override void DoDestroy()
        {
        }
    }
}