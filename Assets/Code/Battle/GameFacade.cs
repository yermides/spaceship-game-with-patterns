using System;
using Code.Ships;
using Code.Ships.Enemies;
using Code.UI;
using NaughtyAttributes;
using UnityEngine;

namespace Code.Battle
{
    public class GameFacade : MonoBehaviour
    {
        [SerializeField] private ShipInstaller shipInstaller;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private ScreenFader screenFader;
        [SerializeField] private GameStateController gameStateController;

        [Button("Start Battle (Only for Play Mode testing)")]

        public void StartBattle()
        {
            // ScoreView.Instance.Reset();
            gameStateController.Reset();
            
            shipInstaller.BuildPlayerShip();
            enemySpawner.StartSpawn();
            screenFader.Hide();
        }

        [Button("Stop Battle (Only for Play Mode testing)")]

        public void StopBattle()
        {
            // shipInstaller.DestroyPlayerShip();
            enemySpawner.StopSpawnAndReset();
            screenFader.Show();
        }

        public void EndBattle()
        {
            // same as stop, but doesn't show the view back
            // shipInstaller.DestroyPlayerShip();
            enemySpawner.StopSpawnAndReset();
        }
    }
}