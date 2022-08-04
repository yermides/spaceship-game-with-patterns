using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Transform _transform;
        private Camera _camera;
        private IInputReceiver _inputReceiver;
        private IMovementConstrainer _movementConstrainer;
        private ShipFiringMediator shipFiringMediator;

        public bool CanFire { get; set; } = true;

        private void Awake()
        {
            _camera = Camera.main;
            _transform = transform;
        }

        public void Configure(IInputReceiver inputReceiver)
        {
            _inputReceiver = inputReceiver;
        }
        
        public void Configure(IMovementConstrainer movementConstrainter)
        {
            _movementConstrainer = movementConstrainter;
        }

        public void Configure(ShipFiringMediator firingMediator)
        {
            shipFiringMediator = firingMediator;
        }

        private void Update()
        {
            Vector2 direction = GetDirection();
            Move(direction);

            if (_inputReceiver.GetRequestForFiring())
            {
                Fire();
            }
        }

        private void Move(Vector2 movement)
        {
            Vector3 movement3d = new Vector3(movement.x, 0, movement.y) * (speed * Time.deltaTime);
            _transform.Translate(movement3d);
            CorrectMovementDegreeOfFreedom();
        }

        private void Fire()
        {
            if (!shipFiringMediator) return;
            
            shipFiringMediator.FireProjectile(_transform.position /* + Vector3.forward * 5.0f */, _transform.forward);
            
            // if (projectilePrefab)
            // {
            //     Instantiate(projectilePrefab, _transform.position, Quaternion.identity);
            // }
        }

        public IEnumerator FireCooldownCoroutine()
        {
            yield return new WaitForSeconds(0.25f);
            CanFire = true;
        }

        private void CorrectMovementDegreeOfFreedom()
        {
            _movementConstrainer.CheckAndCorrectPosition(_transform);
        }

        private Vector2 GetDirection()
        {
            return _inputReceiver.GetDirection();
        }
    }
}