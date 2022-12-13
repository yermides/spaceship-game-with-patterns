using System.Threading.Tasks;
using Code.Ships.Enemies;
using Code.Util;

namespace Code.Common.Commands
{
    public class StopBattleCommand : ICommand
    {
        public Task Execute()
        {
            var enemySpawner = ServiceLocator.Instance.GetService<EnemySpawner>();
            enemySpawner.StopSpawnAndReset();
            return Task.CompletedTask;
        }
    }
}