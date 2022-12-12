using Code.Battle;
using Code.Common.Commands;
using Code.Ships;
using Code.Ships.Common;
using Code.Ships.Enemies;
using Code.Ships.Weapons;
using Code.UI;
using Code.Util;
using UnityEngine;

namespace Code.Core.Installers
{
    [DefaultExecutionOrder(-1)]
    public class GameSceneInstaller : SceneInstallerBehaviour
    {
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private ShipInstaller _shipInstaller;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private GameStateController _gameStateController;
        // [SerializeField] private ScreenFade _screenFade;

        [SerializeField] private ShipFactoryConfiguration shipsConfiguration;
        [SerializeField] private ProjectileFactoryConfiguration projectilesConfiguration;
        
        protected override void InstallAdditionalDependencies()
        {
            ServiceLocator.Instance.RegisterService(_scoreView);
            ServiceLocator.Instance.RegisterService(_shipInstaller);
            ServiceLocator.Instance.RegisterService(_enemySpawner);
            ServiceLocator.Instance.RegisterService(_gameStateController);
            // ServiceLocator.Instance.RegisterService(_screenFade);
            
            InstallProjectileFactory(); 
            InstallShipFactory();
        }
        
        protected override void DoStart()
        {
            new StartBattleCommand().Execute();
        }

        private void InstallShipFactory()
        {
            var shipFactory = new ShipFactory(Instantiate(shipsConfiguration));
            ServiceLocator.Instance.RegisterService(shipFactory);
        }
        
        private void InstallProjectileFactory()
        {
            var projectileFactory = new ProjectileFactory(Instantiate(projectilesConfiguration));
            ServiceLocator.Instance.RegisterService(projectileFactory);
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.DeregisterService<ScoreView>();
            ServiceLocator.Instance.DeregisterService<ShipInstaller>();
            ServiceLocator.Instance.DeregisterService<EnemySpawner>();
            ServiceLocator.Instance.DeregisterService<GameStateController>();
            // ServiceLocator.Instance.DeregisterService<ScreenFade>();
            ServiceLocator.Instance.DeregisterService<ShipFactory>();
            ServiceLocator.Instance.DeregisterService<ProjectileFactory>();
        }
    }
}