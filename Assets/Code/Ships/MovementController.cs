using System;
using Code.Ships.CheckLimits;
using UnityEngine;

namespace Code.Ships
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private Vector3 speed;
        private IShip _ship;
        private Transform _transform;
        private ILimitChecker _limitChecker;

        private void Awake()
        {
            _transform = transform;
        }
        
        public void Configure(IShip ship, ILimitChecker limitChecker, Vector3 desiredSpeed)
        {
            _ship = ship;
            _limitChecker = limitChecker;
            speed = desiredSpeed;
        }
        
        public void Move(Vector3 direction)
        {
            Vector3 movement = new Vector3(
                direction.x * speed.x,
                0.0f,
                direction.z * speed.z
            );
            
            _transform.Translate(movement * Time.deltaTime);
            _limitChecker.ClampFinalPosition();
        }
    }
}