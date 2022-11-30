using Code.Ships.Weapons.Projectiles;
using UnityEngine;

namespace Code.Ships.Weapons
{
    public class ProjectileFactory
    {
        private readonly ProjectileFactoryConfiguration _configuration;

        public ProjectileFactory(ProjectileFactoryConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ProjectileBehaviour Create(string id)
        {
            var projectilePrefab = _configuration.GetProjectileById(id);
            return Object.Instantiate(projectilePrefab);
        }
        
        public ProjectileBehaviour Create(string id, Vector3 position, Quaternion rotation)
        {
            var projectilePrefab = _configuration.GetProjectileById(id);
            return Object.Instantiate(projectilePrefab, position, rotation);
        }
    }
}