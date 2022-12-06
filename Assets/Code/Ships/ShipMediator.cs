using System;
using Code.Input;
using Code.Ships.CheckLimits;
using Code.Ships.Common;
using Code.Ships.Weapons;
using Code.UI;
using UnityEngine;
using NaughtyAttributes;

namespace Code.Ships
{
    [SelectionBase]
    public class ShipMediator : MonoBehaviour, IShip
    {
        [SerializeField] private ShipId id;
        [SerializeField] private MovementController movementController;
        [SerializeField] private WeaponController weaponController;
        [SerializeField] private HealthController healthController;
        
        private Transform _transform;
        private IInputAdapter _inputAdapter;
        private Vector3 _direction;
        private Teams _team;
        private int _score;

        public string Id => id.Value;
        
        public void Configure(ShipConfiguration shipConfiguration)
        {
            _inputAdapter = shipConfiguration.InputAdapter;
            movementController.Configure(this, shipConfiguration.LimitChecker, shipConfiguration.Speed);
            weaponController.Configure(this, shipConfiguration.FireRate, shipConfiguration.DefaultProjectileId, shipConfiguration.Team);
            healthController.Configure(this, shipConfiguration.Health, shipConfiguration.Team);
            _team = shipConfiguration.Team;
            _score = shipConfiguration.Score;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            if (_team == damageable.Team) return;

            // TODO: configure amount of damage that ships deal
            damageable.TakeDamage(1);
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            movementController.Move(_direction, Time.fixedDeltaTime);
        }

        private void Update()
        {
            _direction = _inputAdapter.GetDirection();
            TryFiring();
        }


        private void TryFiring()
        {
            if (_inputAdapter.DidRequestToFire())
            {
                weaponController.TryFiring();
            }
        }

        public void OnDamageReceived(bool hasDied)
        {
            if (!hasDied) return;
            
            ScoreView.Instance.AddScore(_team, _score);
            Destroy(gameObject);
        }
    }
}
