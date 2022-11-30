using NaughtyAttributes;
using UnityEngine;

namespace Code.Ships.Enemies
{
    [CreateAssetMenu(menuName = "ShipGame/LevelConfiguration", fileName = "Level Configuration", order = 0)]
    public class LevelConfiguration : ScriptableObject
    {
        [SerializeField, Label("Enemy Waves Spawn Configuration")] 
        private SpawnConfiguration[] spawnConfigurations;
        
        public SpawnConfiguration[] SpawnConfigurations => spawnConfigurations;
    }
}