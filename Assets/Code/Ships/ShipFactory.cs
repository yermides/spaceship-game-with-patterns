using UnityEngine;

namespace Code.Ships
{
    public class ShipFactory
    {
        private readonly ShipFactoryConfiguration _configuration;

        public ShipFactory(ShipFactoryConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ShipMediator Create(string id)
        {
            var projectilePrefab = _configuration.GetProjectileById(id);
            return Object.Instantiate(projectilePrefab);
        }
        
        public ShipMediator Create(string id, Vector3 position, Quaternion rotation)
        {
            var projectilePrefab = _configuration.GetProjectileById(id);
            return Object.Instantiate(projectilePrefab, position, rotation);
        }
    }
}