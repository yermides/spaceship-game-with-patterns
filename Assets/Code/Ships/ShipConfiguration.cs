using Code.Input;
using Code.Ships.CheckLimits;
using Code.Ships.Weapons;
using UnityEngine;

namespace Code.Ships
{
    public class ShipConfiguration
    {
        public IInputAdapter InputAdapter { get; }
        public ILimitChecker LimitChecker { get; }
        public Vector3 Speed { get; }
        public float FireRate { get; }
        public ProjectileId DefaultProjectileId { get; }

        public ShipConfiguration(IInputAdapter inputAdapter, ILimitChecker limitChecker, 
            Vector3 speed, float fireRate, ProjectileId defaultProjectileId)
        {
            InputAdapter = inputAdapter;
            LimitChecker = limitChecker;
            Speed = speed;
            FireRate = fireRate;
            DefaultProjectileId = defaultProjectileId;
        }
    }
}