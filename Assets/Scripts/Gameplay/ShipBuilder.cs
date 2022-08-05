using UnityEngine;

namespace Gameplay
{
    // TODO: implement all methods & use
    public class ShipBuilder
    {
        private ProjectileFactoryConfiguration _projectileFactoryConfiguration;
        private ProjectileFactory _projectileFactory;
        private ProjectileEnumId _projectileEnumId;
        private IInputReceiver _inputReceiver;
        private float _speed;
        private Vector2 _direction;
        private Ship _ship;

        // public ShipBuilder() { }

        public ShipBuilder WithSpeed(float speed)
        {
            _speed = speed;
            return this;
        }

        public ShipBuilder FromPrefab(Ship ship)
        {
            _ship = ship;
            return this;
        }

        public Ship Build()
        {
            return null;
        }
    }
}