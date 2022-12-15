using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using NaughtyAttributes;
using Code.Input;
using Code.Ships.Common;
using Code.Ships.Weapons.Projectiles;
using Code.Util;

namespace Code.Ships.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        private IShip _ship;
        [SerializeField] private Transform firingPoint;
        [SerializeField, Expandable] private ProjectileFactoryConfiguration projectileFactoryConfiguration;
        [SerializeField] private float fireRate = 0.5f;
        private ProjectileFactory _projectileFactory;
        private List<ProjectileBehaviour> _projectileBehaviourInstances;

        [SerializeField] private ProjectileId projectileToShoot;
        // private float _remainingSecondsToFire;
        
        [SerializeField, Tag]
        private string currentSelectedProjectileId;

        private bool _isAbleToFire = true;
        private IInputAdapter _inputAdapter;
        
        [ShowNonSerializedField]
        private Teams _team;

        private void Awake()
        {
            // _projectileFactory = new ProjectileFactory(Instantiate(projectileFactoryConfiguration));
            _projectileFactory = ServiceLocator.Instance.GetService<ProjectileFactory>();
            _projectileBehaviourInstances = new List<ProjectileBehaviour>();
        }

        public void Configure(IShip ship, float desiredFireRate, ProjectileId defaultProjectileId, Teams team)
        {
            _ship = ship;
            fireRate = desiredFireRate;
            currentSelectedProjectileId = defaultProjectileId.Value;
            _team = team;
        }

        private IEnumerator FiringCooldownCoroutine()
        {
            yield return new WaitForSeconds(fireRate);
            _isAbleToFire = true;
        }

        public void TryFiring()
        {
            if (_isAbleToFire)
            {
                Fire();
            }
        }

        private void Fire()
        {
            // Instantiate and cache the projectile
            // _projectileFactory.Create(testProjectileId, firingPoint.position, firingPoint.rotation);
            
            var projectile = _projectileFactory.Create(currentSelectedProjectileId, firingPoint.position, firingPoint.rotation, _team);
            projectile.onDestroyCallback += OnProjectileDestroyed;
            _projectileBehaviourInstances.Add(projectile);

            // On firing, start cooldown
            _isAbleToFire = false;
            StartCoroutine(FiringCooldownCoroutine());
        }

        private void OnProjectileDestroyed(ProjectileBehaviour projectileBehaviour)
        {
            _projectileBehaviourInstances.Remove(projectileBehaviour);
            projectileBehaviour.onDestroyCallback -= OnProjectileDestroyed;
        }

        private void OnDestroy()
        {
            foreach (var projectileBehaviourInstance in _projectileBehaviourInstances)
            {
                Destroy(projectileBehaviourInstance.gameObject);
            }
            
            _projectileBehaviourInstances.Clear();
        }
    }
}