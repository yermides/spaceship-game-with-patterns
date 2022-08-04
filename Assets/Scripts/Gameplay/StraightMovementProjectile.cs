using UnityEngine;

namespace Gameplay
{
    public class StraightMovementProjectile : Projectile
    {
        protected override void Move()
        {
            _transform.position += direction * (speed * Time.deltaTime);
        }
    }
}