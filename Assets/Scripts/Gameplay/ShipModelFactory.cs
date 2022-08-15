using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay
{
    public class ShipModelFactory
    {
        private readonly ShipModelFactoryConfiguration _configuration;
        
        public ShipModelFactory(ShipModelFactoryConfiguration configuration)
        {
            _configuration = Object.Instantiate(configuration);
        }

        public GameObject Create(ShipModelEnumId id)
        {
            return Object.Instantiate(_configuration.GetPrefabById(id));
        }
    }
}