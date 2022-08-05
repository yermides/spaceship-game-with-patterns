using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private float horizontalSpeed;
        [SerializeField] private float verticalSpeed;
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
            Fire();
        }

        private void Move(Vector2 movement)
        {
            Vector3 movement3d = new Vector3(movement.x, 0, movement.y) * (horizontalSpeed * Time.deltaTime);
            _transform.Translate(movement3d);
            CorrectMovementDegreeOfFreedom();
        }

        private void Fire()
        {
            if (!shipFiringMediator || !_inputReceiver.GetRequestForFiring()) return;
            
            shipFiringMediator.FireProjectile(_transform.position /* + Vector3.forward * 5.0f */, _transform.forward);
        }

        // public void OnFiringResponse()
        // {
        //     CanFire = false;
        //     StartCoroutine(FireCooldownCoroutine());
        // }
        //
        // public IEnumerator FireCooldownCoroutine()
        // {
        //     yield return new WaitForSeconds(shipFiringMediator.FiringCooldownSeconds);
        //     CanFire = true;
        // }

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