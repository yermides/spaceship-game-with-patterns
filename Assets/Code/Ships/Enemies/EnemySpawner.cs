using System;
using System.Collections.Generic;
using Code.Common;
using Code.Common.Events;
using Code.Ships.Common;
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
        // private Dictionary<int, ShipMediator> _spawnedShipsCache; // Instance Id to 

        public void StartSpawn()
        {
            _isAbleToSpawn = true;
        }

        public void StopSpawnAndReset()
        {
            // foreach (var shipMediator in _spawnedShipsCache)
            // {
            //     Destroy(shipMediator.Value.gameObject);
            // }
            //
            // _spawnedShipsCache.Clear();

            _currentTimeInSeconds = 0;
            _currentConfigurationIndex = 0;
            _isAbleToSpawn = false;
        }

        private void Awake()
        {
            // _spawnedShipsCache = new Dictionary<int, ShipMediator>();
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

            if (_currentConfigurationIndex >= levelConfiguration.SpawnConfigurations.Length)
            {
                EventQueue.Instance.EnqueueEvent(new AllShipsSpawnedEvent());
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
                
                EventQueue.Instance.EnqueueEvent(new ShipSpawnedEvent());
            }
        }
        
        /*
        
        private void Start()
        {
            EventQueue.Instance.Subscribe(EventId.ShipDestroyed, this);
        }

        private void OnDestroy()
        {
            EventQueue.Instance.Unsubscribe(EventId.ShipDestroyed, this);
        }
        
        public void Process(EventArgsBase args)
        {
            if (args.EventId != EventId.ShipDestroyed) return;

            var shipDestroyedArgs = (ShipDestroyedEvent)args;

            if (shipDestroyedArgs.team != Teams.Enemy) return;
            
            // _spawnedShipsCache.Remove(shipDestroyedArgs.instanceId);
        }
        
        */
        
    }
}
