using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class GameInstaller : MonoBehaviour
    {
        private ShipBuilder _shipBuilder;
        private ProjectileFactory _projectileFactory;
        [SerializeField] private ProjectileFactoryConfiguration projectileFactoryConfiguration;
        [SerializeField] private Ship shipPrototypePrefab;
        
        private void Awake()
        {
            // ShipBuilder.SharedPrototypeShip = shipPrototypePrefab;
            
            _projectileFactory = new ProjectileFactory(projectileFactoryConfiguration);
            _shipBuilder = new ShipBuilder()
                .WithPosition(Vector3.zero)
                .WithRotation(Quaternion.identity)
                .WithSpeed(10)
                .WithProjectileFactory(_projectileFactory)
                .WithProjectileType(ProjectileEnumId.Basic)
                .WithPrototypePrefab(shipPrototypePrefab)
                .WithInputReceiver(new UnityInputAdapter())
                ;
        }

        private void Update()
        {
            // Test, spawn on key press
            if (Input.GetKeyDown(KeyCode.E))
            {
                _shipBuilder.Build();
            }
        }
    }
}
