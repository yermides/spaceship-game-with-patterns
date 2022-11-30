using System;
using Code.Input;
using Code.Ships.CheckLimits;
using Code.Ships.Weapons;
using UnityEngine;
using NaughtyAttributes;

namespace Code.Ships
{
    [SelectionBase]
    public class ShipMediator : MonoBehaviour, IShip
    {
        [SerializeField, Expandable] private ShipId id;
        [SerializeField] private MovementController movementController;
        [SerializeField] private WeaponController weaponController;
        private Transform _transform;
        private IInputAdapter _inputAdapter;
        
        public string Id => id.Value;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            var direction = _inputAdapter.GetDirection();
            movementController.Move(direction);
            TryFiring();
        }

        public void Configure(IInputAdapter inputAdapter, ILimitChecker limitChecker)
        {
            _inputAdapter = inputAdapter;
            movementController.Configure(this, limitChecker);
            weaponController.Configure(this);
        }

        private void TryFiring()
        {
            if (_inputAdapter.DidRequestToFire())
            {
                weaponController.TryFiring();
            }
        }
    }
}
