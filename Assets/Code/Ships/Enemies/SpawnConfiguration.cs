using System;
using NaughtyAttributes;
using UnityEngine;

namespace Code.Ships.Enemies
{
    [Serializable]
    public class SpawnConfiguration
    {
        [SerializeField] private ShipToSpawnConfiguration[] shipToSpawnConfiguration;
        [SerializeField] private float timeToSpawn;
        
        public ShipToSpawnConfiguration[] ShipToSpawnConfiguration => shipToSpawnConfiguration;
        public float TimeToSpawn => timeToSpawn;
    }
}