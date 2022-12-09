using Code.Common;
using Code.Common.Events;
using Code.Input;
using Code.Ships.CheckLimits;
using Code.Ships.Common;
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

    public enum ShipCheckDestroyLimitsStrategy
    {
        CheckBottomLimitsStrategy,
        NotCheckDestroyLimitsStrategy
    }

    public class ShipInstaller : MonoBehaviour
    {
        [SerializeField, Expandable] private ShipToSpawnConfiguration shipConfiguration;
        [SerializeField, Expandable] private ShipFactoryConfiguration shipsFactoryConfiguration;
        private ShipBuilder _shipBuilder;
        
        private void Awake()
        {
            var shipFactory = new ShipFactory(Instantiate(shipsFactoryConfiguration));
            
            _shipBuilder = shipFactory
                .Create(shipConfiguration.ShipId)
                .WithTeam(Teams.Ally)
                .WithConfiguration(shipConfiguration);

            SetInput(_shipBuilder);
            SetCheckLimitsStrategy(_shipBuilder);
            SetCheckDestroyLimitsStrategy(_shipBuilder);

            // BuildPlayerShip();
        }

        private void SetCheckDestroyLimitsStrategy(ShipBuilder shipBuilder)
        {
            shipBuilder.WithCheckDestroyLimitsStrategy(ShipCheckDestroyLimitsStrategy.NotCheckDestroyLimitsStrategy);
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
        public void BuildPlayerShip()
        {
            _shipBuilder.Build();
        }
    }
}