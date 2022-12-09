using Code.Input;
using Code.Ships.CheckDestroyLimits;
using Code.Ships.CheckLimits;
using Code.Ships.Enemies;
using UnityEngine;

namespace Code.Ships.Common
{
    public class ShipBuilder
    {
        private readonly Camera _camera;
        private ShipMediator _shipPrefab;
        private Vector3 _position;
        private Quaternion _rotation;
        private ShipInputStrategy _inputStrategy;
        private ShipCheckLimitsStrategy _checkLimitsStrategy;
        private ShipCheckDestroyLimitsStrategy _checkDestroyLimitsStrategy;
        private ShipToSpawnConfiguration _shipToSpawnConfiguration;
        private int _health = 3;
        private Teams _team;

        public ShipBuilder()
        {
            _position = Vector3.zero;
            _rotation = Quaternion.identity;
            _camera = Camera.main;
        }

        public ShipBuilder FromPrefab(ShipMediator shipPrefab)
        {
            _shipPrefab = shipPrefab;
            return this;
        }
        
        public ShipBuilder WithPosition(Vector3 position)
        {
            _position = position;
            return this;
        }
        
        public ShipBuilder WithRotation(Quaternion rotation)
        {
            _rotation = rotation;
            return this;
        }

        public ShipBuilder WithHealth(int health)
        {
            _health = health;
            return this;
        }    
        
        public ShipBuilder WithTeam(Teams team)
        {
            _team = team;
            return this;
        }      

        public ShipBuilder WithInputStrategy(ShipInputStrategy inputStrategy)
        {
            _inputStrategy = inputStrategy;
            return this;
        }
        
        public ShipBuilder WithCheckLimitsStrategy(ShipCheckLimitsStrategy checkLimitsStrategy)
        {
            _checkLimitsStrategy = checkLimitsStrategy;
            return this;
        }
        
        public ShipBuilder WithCheckDestroyLimitsStrategy(ShipCheckDestroyLimitsStrategy checkDestroyLimitsStrategy)
        {
            _checkDestroyLimitsStrategy = checkDestroyLimitsStrategy;
            return this;
        }

        public ShipBuilder WithConfiguration(ShipToSpawnConfiguration shipToSpawnConfiguration)
        {
            _shipToSpawnConfiguration = shipToSpawnConfiguration;
            return this;
        }

        public ShipMediator Build()
        {
            var ship = Object.Instantiate(_shipPrefab, _position, _rotation);

            var configuration = new ShipConfiguration(
                GetInputStrategy(ship),
                GetLimitCheckerStrategy(ship),
                _shipToSpawnConfiguration.Speed, 
                _shipToSpawnConfiguration.FireRate,
                _shipToSpawnConfiguration.ProjectileId,
                _shipToSpawnConfiguration.Health,
                _team,
                _shipToSpawnConfiguration.Score,
                GetCheckDestroyLimitsStrategy()
            );
                
            // ship.Configure(GetInputStrategy(ship), GetLimitCheckerStrategy(ship));
            ship.Configure(configuration);
            
            return ship;
        }

        private IInputAdapter GetInputStrategy(ShipMediator shipMediator)
        {
            return _inputStrategy switch
            {
                ShipInputStrategy.UseUnityInput => new UnityInputAdapter(),
                ShipInputStrategy.UseAIInput => new AIInputAdapter(shipMediator),
                _ => null
            };
        }

        private ILimitChecker GetLimitCheckerStrategy(ShipMediator shipMediator)
        {
            return _checkLimitsStrategy switch
            {
                ShipCheckLimitsStrategy.ViewportLimitChecker => new ViewportLimitChecker(_camera),
                ShipCheckLimitsStrategy.InitialPositionLimitChecker => new InitialPositionLimitChecker(
                    shipMediator.transform, 10.0f),
                _ => null
            };
        }

        private ICheckDestroyLimits GetCheckDestroyLimitsStrategy()
        {
            return _checkDestroyLimitsStrategy switch
            {
                ShipCheckDestroyLimitsStrategy.CheckBottomLimitsStrategy => new CheckBottomLimitsStrategy(),
                ShipCheckDestroyLimitsStrategy.NotCheckDestroyLimitsStrategy => new NotCheckDestroyLimitsStrategy(),
                _ => null
            };
        }
        


        
    }
}