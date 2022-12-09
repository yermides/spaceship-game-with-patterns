using System;
using Code.Common;
using Code.Common.Events;
using Code.Input;
using Code.Ships.CheckDestroyLimits;
using Code.Ships.CheckLimits;
using Code.Ships.Common;
using Code.Ships.Weapons;
using Code.UI;
using UnityEngine;
using NaughtyAttributes;

namespace Code.Ships
{
    [SelectionBase]
    public class ShipMediator : MonoBehaviour, IShip, IEventObserver
    {
        [SerializeField] private ShipId id;
        [SerializeField] private MovementController movementController;
        [SerializeField] private WeaponController weaponController;
        [SerializeField] private HealthController healthController;
        
        private IInputAdapter _inputAdapter;
        private ICheckDestroyLimits _checkDestroyLimits;
        private Vector3 _direction;
        private Teams _team;
        private int _score;
        private Transform _transform;

        public string Id => id.Value;
        
        public void Configure(ShipConfiguration shipConfiguration)
        {
            _inputAdapter = shipConfiguration.InputAdapter;
            movementController.Configure(this, shipConfiguration.LimitChecker, shipConfiguration.Speed);
            weaponController.Configure(this, shipConfiguration.FireRate, shipConfiguration.DefaultProjectileId, shipConfiguration.Team);
            healthController.Configure(this, shipConfiguration.Health, shipConfiguration.Team);
            _team = shipConfiguration.Team;
            _score = shipConfiguration.Score;
            _checkDestroyLimits = shipConfiguration.LimitDestroyChecker;
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

        private void Start()
        {
            EventQueue.Instance.Subscribe(EventId.GameOver, this);
        }

        private void OnDestroy()
        {
            EventQueue.Instance.Unsubscribe(EventId.GameOver, this);
        }

        private void FixedUpdate()
        {
            movementController.Move(_direction, Time.fixedDeltaTime);
        }

        private void Update()
        {
            _direction = _inputAdapter.GetDirection();
            TryFiring();

            if (_checkDestroyLimits.IsWithinLimits(_transform.position)) return;
            
            DestroyShip();
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

            DestroyShip();
        }

        private void DestroyShip()
        {
            Destroy(gameObject);
            EventQueue.Instance.EnqueueEvent(new ShipDestroyedEvent(_team, _score, GetInstanceID()));
        }

        public void Process(EventArgsBase args)
        {
            if (args.EventId != EventId.GameOver) return;
            Destroy(gameObject);
        }
    }
}
