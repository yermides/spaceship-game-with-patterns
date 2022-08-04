using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class ShipFiringMediator : MonoBehaviour
    {
        [Header("Ship Reference")]
        [SerializeField] private Ship ship;
        
        [Header("Projectile params")]
        [SerializeField] private ProjectileFactoryConfiguration projectileFactoryConfiguration;
        [SerializeField] private ProjectileEnumId projectileTypeToSpawn;
        [SerializeField] private float spawnOffset;
        private ProjectileFactory _projectileFactory;
        
        private void Awake()
        {
            _projectileFactory = new ProjectileFactory(projectileFactoryConfiguration);
            ship.Configure(this);
        }

        public void FireProjectile(Vector3 position, Vector3 direction)
        {
            if (!ship.CanFire) return;

            // Projectile projectile = Instantiate(this.projectile, position + (direction * spawnOffset), Quaternion.identity);
            Projectile projectile = _projectileFactory.Create(projectileTypeToSpawn);
            
            projectile.Configure(this);
            projectile.Position = position;
            projectile.Direction = direction;
            
            // ship.onProjectileFired?.Invoke maybe, though I can do it directly

            ship.CanFire = false;
            StartCoroutine(ship.FireCooldownCoroutine());
        }
    }
}