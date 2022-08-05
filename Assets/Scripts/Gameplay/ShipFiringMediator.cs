using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class ShipFiringMediator : MonoBehaviour
    {
        [Header("Ship Reference")]
        [SerializeField] private Ship ship;
        [SerializeField] private float firingCooldownSeconds = 0.25f;
        // [SerializeField] private bool canFire = true;
        
        [Header("Projectile params")]
        [SerializeField] private ProjectileFactoryConfiguration projectileFactoryConfiguration;
        [SerializeField] private ProjectileEnumId projectileTypeToSpawn;
        [SerializeField] private float spawnOffset;
        private ProjectileFactory _projectileFactory;

        public float FiringCooldownSeconds
        {
            get => firingCooldownSeconds;
        }

        private void Awake()
        {
            if (!ship)
            {
                ship = GetComponent<Ship>();
            }

            _projectileFactory = new ProjectileFactory(projectileFactoryConfiguration);
            ship.Configure(this);
        }

        public void FireProjectile(Vector3 position, Vector3 direction)
        {
            // if (!canFire) return;
            if (!ship.CanFire) return;

            // Projectile applied response
            Projectile projectile = _projectileFactory.Create(projectileTypeToSpawn);
            projectile.Configure(this);
            projectile.Position = position;
            projectile.Direction = direction;
            
            Physics.IgnoreCollision(projectile.GetComponent<Collider>(), ship.GetComponent<Collider>());
            
            // Ship applied response
            StartCoroutine(ShipFiringCooldownCoroutine());

            // ship.onProjectileFired?.Invoke maybe, though I can do it directly
        }

        private IEnumerator ShipFiringCooldownCoroutine()
        {
            ship.CanFire = false;
            yield return new WaitForSeconds(firingCooldownSeconds);
            ship.CanFire = true;
        }
    }
}