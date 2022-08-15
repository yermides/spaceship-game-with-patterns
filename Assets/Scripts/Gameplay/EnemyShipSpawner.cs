using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class EnemyShipSpawner : MonoBehaviour
    {
        [Header("Ship Factory Facade")]
        [SerializeField] private ProjectileFactoryConfiguration projectileFactoryConfiguration;
        private ProjectileFactory _projectileFactory;

        [Header("Spawning Params")] 
        [SerializeField] private int shipsToSpawnCount;
        [SerializeField] private float secondsBetweenSpawn;
        [SerializeField] private Ship shipPrototypePrefab;
        private ShipBuilder _shipBuilder;

        private List<Ship> _enemies;

        private void Awake()
        {
            _enemies = new List<Ship>();
            
            Quaternion facingPlayerOrientation = Quaternion.Euler(new Vector3(0, 180, 0));
            
            _projectileFactory = new ProjectileFactory(projectileFactoryConfiguration);
            _shipBuilder = new ShipBuilder()
                    .WithPosition(Vector3.forward * 4.0f)
                    .WithRotation(facingPlayerOrientation)
                    .WithSpeed(2.5f)
                    .WithProjectileFactory(_projectileFactory)
                    .WithProjectileType(ProjectileEnumId.Basic)
                    .WithPrototypePrefab(shipPrototypePrefab)
                    .WithInputReceiver(new UnityInputAdapter())
                ;
        }

        public void SpawnEnemies()
        {
            StartCoroutine(SpawnShipCoroutine());
        }

        public void DestroyEnemies()
        {
            foreach (var enemy in _enemies)
            {
                Destroy(enemy.gameObject);
            }
        }

        private IEnumerator SpawnShipCoroutine()
        {
            while (shipsToSpawnCount > 0)
            {
                yield return new WaitForSeconds(secondsBetweenSpawn);
                Ship spawnedShip = _shipBuilder.Build();
                _enemies.Add(spawnedShip);
                --shipsToSpawnCount;
            }
        }

        // private Ship SpawnShip()
        // {
        //     NotImplementedException();
        // }
    }
}
