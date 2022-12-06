using System;
using Code.Ships.CheckLimits;
using UnityEngine;

namespace Code.Ships
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbodyReference;
        [SerializeField] private Vector3 speed;
        private IShip _ship;
        private ILimitChecker _limitChecker;
        
        public void Configure(IShip ship, ILimitChecker limitChecker, Vector3 desiredSpeed)
        {
            _ship = ship;
            _limitChecker = limitChecker;
            speed = desiredSpeed;
        }
        
        public void Move(Vector3 direction, float deltaTime)
        {
            Vector3 movement = new Vector3(
                direction.x * speed.x,
                0.0f,
                direction.z * speed.z
            );

            Vector3 targetPosition = rigidbodyReference.position + (movement * deltaTime);
            targetPosition = _limitChecker.ClampFinalPosition(targetPosition);
            rigidbodyReference.MovePosition(targetPosition);
        }
    }
}