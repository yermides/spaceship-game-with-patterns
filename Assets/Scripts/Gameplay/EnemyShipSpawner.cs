using System;
using System.Collections;
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
        private ShipBuilder _shipBuilder;
        
        private void Awake()
        {
            _projectileFactory = new ProjectileFactory(projectileFactoryConfiguration);
            // TODO:
            // _shipBuilder = new ShipBuilder();
        }

        private IEnumerator SpawnShipCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(secondsBetweenSpawn);
                
            }
        }

        // private Ship SpawnShip()
        // {
        //     NotImplementedException();
        // }
    }
}
