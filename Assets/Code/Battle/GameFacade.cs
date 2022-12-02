using System;
using Code.Ships;
using Code.Ships.Enemies;
using UnityEngine;

namespace Code.Battle
{
    public class GameFacade : MonoBehaviour
    {
        [SerializeField] private ShipInstaller shipInstaller;
        [SerializeField] private EnemySpawner enemySpawner;
        // [SerializeField] private MenuController menuController;

        private void StartBattle()
        {
            throw new NotImplementedException();
        }

        private void StopBattle()
        {
            throw new NotImplementedException();
        }
    }
}