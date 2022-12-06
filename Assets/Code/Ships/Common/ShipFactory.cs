using UnityEngine;

namespace Code.Ships.Common
{
    public class ShipFactory
    {
        private readonly ShipFactoryConfiguration _configuration;

        public ShipFactory(ShipFactoryConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ShipBuilder Create(string id)
        {
            return Create(id, Vector3.zero, Quaternion.identity);
        }
        
        public ShipBuilder Create(string id, Vector3 position, Quaternion rotation)
        {
            var projectilePrefab = _configuration.GetProjectileById(id);
            // var projectileInstance = Object.Instantiate(projectilePrefab, position, rotation);
            var shipBuilder = new ShipBuilder()
                .FromPrefab(projectilePrefab)
                .WithPosition(position)
                .WithRotation(rotation);
            
            return shipBuilder;
        }
    }
}