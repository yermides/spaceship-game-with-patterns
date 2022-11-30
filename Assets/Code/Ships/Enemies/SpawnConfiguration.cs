using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Ships.Enemies
{
    [Serializable]
    public class SpawnConfiguration
    {
        [SerializeField, Expandable] private ShipToSpawnConfiguration[] shipToSpawnConfiguration;
        [SerializeField] private float timeToSpawn;
        
        public ShipToSpawnConfiguration[] ShipToSpawnConfiguration => shipToSpawnConfiguration;
        public float TimeToSpawn => timeToSpawn;
    }
}