using System;
using NaughtyAttributes;
using UnityEngine;

namespace Code.Ships.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField, Expandable, AllowNesting] 
        private LevelConfiguration levelConfiguration;
        
        [SerializeField, Expandable] 
        private ShipFactoryConfiguration shipFactoryConfiguration;
        
        private ShipFactory _shipFactory;
        private float _currentTimeInSeconds;
        private int _currentConfigurationIndex;

        private void Awake()
        {
            _shipFactory = new ShipFactory(Instantiate(shipFactoryConfiguration));
        }

        private void Update()
        {
            if (_currentConfigurationIndex >= levelConfiguration.SpawnConfigurations.Length) return;
            
            _currentTimeInSeconds += Time.deltaTime;
            
            var enemyWave = levelConfiguration.SpawnConfigurations[_currentConfigurationIndex];

            if (_currentTimeInSeconds > enemyWave.TimeToSpawn)
            {
                SpawnEnemyWave(enemyWave);
                _currentConfigurationIndex += 1;
                _currentTimeInSeconds = 0;
            }
        }

        private void SpawnEnemyWave(SpawnConfiguration enemyWave)
        {
            foreach (var shipToSpawnConfiguration in enemyWave.ShipToSpawnConfiguration)
            {
                _shipFactory.Create(shipToSpawnConfiguration.ShipId, shipToSpawnConfiguration.Position, Quaternion.identity);
            }
        }
    }
}