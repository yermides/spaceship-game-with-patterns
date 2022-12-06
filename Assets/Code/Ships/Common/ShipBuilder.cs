using Code.Input;
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
                _shipToSpawnConfiguration.Score
            );
                
            // ship.Configure(GetInputStrategy(ship), GetLimitCheckerStrategy(ship));
            ship.Configure(configuration);
            
            return ship;
        }

        private IInputAdapter GetInputStrategy(ShipMediator shipMediator)
        {
            if (_inputStrategy == ShipInputStrategy.UseUnityInput)
            {
                return new UnityInputAdapter();
            } 
            else if (_inputStrategy == ShipInputStrategy.UseAIInput)
            {
                return new AIInputAdapter(shipMediator);
            }

            return null;
        }

        private ILimitChecker GetLimitCheckerStrategy(ShipMediator shipMediator)
        {
            if(_checkLimitsStrategy == ShipCheckLimitsStrategy.ViewportLimitChecker)
            {
                return new ViewportLimitChecker(_camera);
            } 
            else if (_checkLimitsStrategy == ShipCheckLimitsStrategy.InitialPositionLimitChecker)
            {
                return new InitialPositionLimitChecker(shipMediator.transform, 10.0f);
            }

            return null;
        }
    }
}