using UnityEngine;

namespace Gameplay
{
    public class BattleFacade : MonoBehaviour
    {
        [SerializeField] private EnemyShipSpawner shipSpawner;
        // IngameGui
            
        public void StartBattle()
        {
            shipSpawner.SpawnEnemies();
        }

        public void EndBattle()
        {
            shipSpawner.DestroyEnemies();
        }
    }
}