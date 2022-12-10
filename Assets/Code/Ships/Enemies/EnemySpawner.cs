using System;
using System.Collections.Generic;
using Code.Common;
using Code.Common.Events;
using Code.Ships.Common;
using Code.Util;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Ships.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] possibleSpawnPoints;
        
        [SerializeField, Expandable, AllowNesting] 
        private LevelConfiguration levelConfiguration;
        
        [SerializeField, Expandable] 
        private ShipFactoryConfiguration shipFactoryConfiguration;
        
        private ShipFactory _shipFactory;
        private float _currentTimeInSeconds;
        private int _currentConfigurationIndex;
        private bool _isAbleToSpawn;

        public void StartSpawn()
        {
            _isAbleToSpawn = true;
        }

        public void StopSpawnAndReset()
        {
            _currentTimeInSeconds = 0;
            _currentConfigurationIndex = 0;
            _isAbleToSpawn = false;
        }

        private void Awake()
        {
            _shipFactory = new ShipFactory(Instantiate(shipFactoryConfiguration));
        }

        private void Update()
        {
            if (!_isAbleToSpawn) return;
            if (_currentConfigurationIndex >= levelConfiguration.SpawnConfigurations.Length) return;
            
            _currentTimeInSeconds += Time.deltaTime;
            
            var enemyWave = levelConfiguration.SpawnConfigurations[_currentConfigurationIndex];

            if (enemyWave.TimeToSpawn > _currentTimeInSeconds) return;
            
            SpawnEnemyWave(enemyWave);
            _currentConfigurationIndex += 1;

            // enqueue event if it spawned wave happens to be the last one
            if (_currentConfigurationIndex >= levelConfiguration.SpawnConfigurations.Length)
            {
                var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
                eventQueue.Enqueue(new AllShipsSpawnedEvent());
            }
        }

        private void SpawnEnemyWave(SpawnConfiguration enemyWave)
        {
            foreach (var shipToSpawnConfiguration in enemyWave.ShipToSpawnConfiguration)
            {
                var spawnPointIndex = Random.Range(0, possibleSpawnPoints.Length);
                var spawnPoint = possibleSpawnPoints[spawnPointIndex];
                
                var shipBuilder = _shipFactory.Create(
                    shipToSpawnConfiguration.ShipId, 
                    spawnPoint.position, 
                    spawnPoint.rotation
                );

                var spawnedShip = shipBuilder.WithConfiguration(shipToSpawnConfiguration)
                    .WithInputStrategy(ShipInputStrategy.UseAIInput)
                    .WithCheckLimitsStrategy(ShipCheckLimitsStrategy.ViewportLimitChecker)
                    .WithTeam(Teams.Enemy)
                    .WithCheckDestroyLimitsStrategy(ShipCheckDestroyLimitsStrategy.CheckBottomLimitsStrategy)
                    .Build();
                
                var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
                eventQueue.Enqueue(new ShipSpawnedEvent());
            }
        }
    }
}
