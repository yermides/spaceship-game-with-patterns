using System;
using Code.Common;
using Code.Common.Events;
using Code.Input;
using Code.Ships.CheckDestroyLimits;
using Code.Ships.CheckLimits;
using Code.Ships.Common;
using Code.Ships.Weapons;
using Code.UI;
using Code.Util;
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
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Subscribe<GameOverEvent>(OnGameOverEvent);
            eventQueue.Subscribe<VictoryEvent>(OnVictoryEvent);
        }

        private void OnVictoryEvent(VictoryEvent obj)
        {
            Destroy(gameObject);
        }

        private void OnGameOverEvent(GameOverEvent evt)
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Unsubscribe<GameOverEvent>(OnGameOverEvent);
            eventQueue.Unsubscribe<VictoryEvent>(OnVictoryEvent);
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
            
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Enqueue(new ShipDestroyedEvent(_team, _score, GetInstanceID()));
        }
    }
}
