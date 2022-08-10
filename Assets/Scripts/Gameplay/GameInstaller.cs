using System;
using UnityEngine;

namespace Gameplay
{
    public class GameInstaller : MonoBehaviour
    {
        private ShipBuilder _shipBuilder;
        private ProjectileFactory _projectileFactory;
        [SerializeField] private ProjectileFactoryConfiguration _projectileFactoryConfiguration;
        
        private void Awake()
        {
            _projectileFactory = new ProjectileFactory(_projectileFactoryConfiguration);
            _shipBuilder = new ShipBuilder()
                .WithPosition(Vector3.zero)
                .WithRotation(Quaternion.identity)
                .WithSpeed(10)
                .WithInputReceiver(new UnityInputAdapter())
                .WithProjectileType(ProjectileEnumId.Basic)
                .WithProjectileFactory(_projectileFactory);
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
