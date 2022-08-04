using System;
using System.Collections;
using System.Collections.Generic;
using Tags;
using UnityEngine;

namespace Gameplay
{
    // Simple projectile
    public class Projectile : MonoBehaviour
    {
        protected Transform _transform;
        [SerializeField] protected float speed;
        protected Vector3 direction;
        protected ShipFiringMediator shipFiringMediator;

        public Vector3 Direction
        {
            get => direction;
            set => direction = value;
        }

        public Vector3 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        private void Awake()
        {
            _transform = transform;
        }

        public void Configure(ShipFiringMediator firingMediator)
        {
            shipFiringMediator = firingMediator;
        }

        private void Update()
        {
            Move();
        }

        protected virtual void Move()
        {
            // generic projectile movement
            _transform.position += direction * (speed * Time.deltaTime);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject other = collision.gameObject;
            Debug.Log("I collided with other");

            if (other.TryGetComponent(out TagEnemy _))
            { 
                // Maybe notify impact to the player ship
                // shipFiringMediator.TriggerEnemyHitEvent(); or something
                Destroy(other);
                Destroy(gameObject);
            }
        }
    }
}
