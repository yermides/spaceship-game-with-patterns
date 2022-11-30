using System;
using Code.Ships.CheckLimits;
using UnityEngine;

namespace Code.Ships
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float speed = 5.0f;
        private IShip _ship;
        private Transform _transform;
        private ILimitChecker _limitChecker;

        private void Awake()
        {
            _transform = transform;
        }
        
        public void Configure(IShip ship, ILimitChecker limitChecker)
        {
            _ship = ship;
            _limitChecker = limitChecker;
        }
        
        public void Move(Vector3 direction)
        {
            _transform.Translate(direction * (speed * Time.deltaTime));
            _limitChecker.ClampFinalPosition();
        }
    }
}