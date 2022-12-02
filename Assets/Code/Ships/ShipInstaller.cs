using Code.Input;
using Code.Ships.CheckLimits;
using Code.Ships.Enemies;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Ships
{
    public enum ShipInputStrategy
    {
        UseUnityInput,
        UseAIInput
    }

    public enum ShipCheckLimitsStrategy
    {
        ViewportLimitChecker,
        InitialPositionLimitChecker
    }

    public class ShipInstaller : MonoBehaviour
    {
        // [SerializeField] private ShipMediator shipMediatorReference;
        // [SerializeField] private ShipInputStrategy shipInputStrategy;
        // [SerializeField] private 
        
        [SerializeField, Expandable] private ShipToSpawnConfiguration shipConfiguration;
        [SerializeField, Expandable] private ShipFactoryConfiguration shipsFactoryConfiguration;
        private ShipBuilder _shipBuilder;
        private ShipMediator _playerShip;
        
        private void Awake()
        {
            var shipFactory = new ShipFactory(Instantiate(shipsFactoryConfiguration));
            
            _shipBuilder = shipFactory
                .Create(shipConfiguration.ShipId)
                .WithConfiguration(shipConfiguration);
            SetInput(_shipBuilder);
            SetCheckLimitsStrategy(_shipBuilder);

            BuildShip();
        }
        
        private void SetInput(ShipBuilder shipBuilder)
        {
            shipBuilder.WithInputStrategy(ShipInputStrategy.UseUnityInput);
        }
        
        private void SetCheckLimitsStrategy(ShipBuilder shipBuilder)
        {
            shipBuilder.WithCheckLimitsStrategy(ShipCheckLimitsStrategy.ViewportLimitChecker);
        }

        [Button("Build Ship (Only for Play Mode testing)")]
        private void BuildShip()
        {
            _playerShip = _shipBuilder.Build();
        }
        
        [Button("Destroy Ship (Only for Play Mode testing)")]
        
        private void DestroyShip()
        {
            if (!_playerShip) return;
            
            Destroy(_playerShip.gameObject);
        }
    }
}