using UnityEngine;

namespace Code.Ships.Weapons.Projectiles
{
    public class LinearProjectile : ProjectileBehaviour
    {
        [SerializeField] private float speed = 10.0f;

        protected override void DoStart()
        {
            rigidbodyReference.velocity = transform.forward * speed;
        }

        protected override void DoMove()
        {
        }

        protected override void DoDestroy()
        {
        }
    }
}