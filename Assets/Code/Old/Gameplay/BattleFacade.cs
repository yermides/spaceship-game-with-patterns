using UnityEngine;

namespace Gameplay
{
    public class BattleFacade : MonoBehaviour
    {
        [SerializeField] private EnemyShipSpawner shipSpawner;
        [SerializeField] private bool startBattleOnAwake;
        bool isBattleActive = false;
        // IngameGui

        private void Awake() 
        {
            if(!startBattleOnAwake) return;

            StartBattle();
        }
            
        public void StartBattle()
        {
            if(isBattleActive) return;

            shipSpawner.SpawnEnemies();
            isBattleActive = true;
        }

        public void EndBattle()
        {
            if(!isBattleActive) return;

            shipSpawner.DestroyEnemies();
            isBattleActive = false;
        }
    }
}