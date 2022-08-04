using System;
using UnityEngine;

namespace Gameplay
{
    public class IncrementalMovementProjectile : Projectile
    {
        private float startSpeed;
        [SerializeField] private float maxSpeed = 20;
        [SerializeField] private float accelerationRate = 1.0f;

        private void Start()
        {
            startSpeed = speed;
        }

        protected override void Move()
        {
            speed += accelerationRate * Time.deltaTime;
            speed = Mathf.Clamp(speed, startSpeed, maxSpeed);
            base.Move();
        }
    }
}