using System.Threading.Tasks;
using Code.Battle;
using Code.Ships;
using Code.Ships.Enemies;
using Code.UI;
using Code.Util;

namespace Code.Common.Commands
{
    public class StartBattleCommand : ICommand
    {
        public Task Execute()
        {
            var enemySpawner = ServiceLocator.Instance.GetService<EnemySpawner>();
            var gameStateController = ServiceLocator.Instance.GetService<GameStateController>();
            var shipInstaller = ServiceLocator.Instance.GetService<ShipInstaller>();
            var scoreView = ServiceLocator.Instance.GetService<ScoreView>();
            
            scoreView.Reset();
            gameStateController.Reset();
            shipInstaller.BuildPlayerShip();
            enemySpawner.StartSpawn();
            
            return Task.CompletedTask;
        }
    }
}