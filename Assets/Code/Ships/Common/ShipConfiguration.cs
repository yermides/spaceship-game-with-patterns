using Code.Input;
using Code.Ships.CheckLimits;
using Code.Ships.Weapons;
using UnityEngine;

namespace Code.Ships.Common
{
    public class ShipConfiguration
    {
        private IInputAdapter _inputAdapter;
        private ILimitChecker _limitChecker;
        private Vector3 _speed;
        private float _fireRate;
        private ProjectileId _defaultProjectileId;
        private int _health;
        private Teams _team;
        private int _score;

        public ShipConfiguration(IInputAdapter inputAdapter, ILimitChecker limitChecker, 
            Vector3 speed, float fireRate, ProjectileId defaultProjectileId, int health, Teams team,
            int score)
        {
            _inputAdapter = inputAdapter;
            _limitChecker = limitChecker;
            _speed = speed;
            _fireRate = fireRate;
            _defaultProjectileId = defaultProjectileId;
            _health = health;
            _team = team;
            _score = score;
        }
        
        public IInputAdapter InputAdapter => _inputAdapter;
        public ILimitChecker LimitChecker => _limitChecker;
        public Vector3 Speed => _speed;
        public float FireRate => _fireRate;
        public ProjectileId DefaultProjectileId => _defaultProjectileId;
        public int Health => _health;
        public Teams Team => _team;
        public int Score => _score;

    }
}