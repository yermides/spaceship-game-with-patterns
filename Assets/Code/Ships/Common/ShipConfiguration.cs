using Code.Input;
using Code.Ships.CheckDestroyLimits;
using Code.Ships.CheckLimits;
using Code.Ships.Weapons;
using UnityEngine;

namespace Code.Ships.Common
{
    public class ShipConfiguration
    {
        public ShipConfiguration
        (
            IInputAdapter inputAdapter, 
            ILimitChecker limitChecker, 
            Vector3 speed, 
            float fireRate, 
            ProjectileId defaultProjectileId, 
            int health, 
            Teams team,
            int score, 
            ICheckDestroyLimits limitDestroyer
        )
        {
            InputAdapter = inputAdapter;
            LimitChecker = limitChecker;
            Speed = speed;
            FireRate = fireRate;
            DefaultProjectileId = defaultProjectileId;
            Health = health;
            Team = team;
            Score = score;
            LimitDestroyChecker = limitDestroyer;
        }
        
        public IInputAdapter InputAdapter { get; }

        public ILimitChecker LimitChecker { get; }

        public Vector3 Speed { get; }

        public float FireRate { get; }

        public ProjectileId DefaultProjectileId { get; }

        public int Health { get; }

        public Teams Team { get; }

        public int Score { get; }

        public ICheckDestroyLimits LimitDestroyChecker { get; }
    }
}