using System;
using UnityEngine;

namespace Gameplay
{
    public class WaveMovementProjectile : Projectile
    {
        [SerializeField] private float maxArcValue = 1.0f;
        private Vector3 _initialPosition;
        
        private void Start()
        {
            _initialPosition = _transform.position;
        }

        protected override void Move()
        {
            // normal movement based on direction
            Vector3 positionOffset = direction * (speed * Time.deltaTime);
            
            // use sin and/or cosin to make an arc
            Vector3 position = _transform.position;
            
            // obviously improve, but the concept is there
            positionOffset.x = Mathf.Sin((position - _initialPosition).sqrMagnitude / 5.0f);
            position += positionOffset;
            
            _transform.position = position;
        }
    }
}