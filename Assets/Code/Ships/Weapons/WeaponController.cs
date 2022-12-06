using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using Code.Input;
using Code.Ships.Common;

namespace Code.Ships.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        private IShip _ship;
        [SerializeField] private Transform firingPoint;
        [SerializeField, Expandable] private ProjectileFactoryConfiguration projectileFactoryConfiguration;
        [SerializeField] private float fireRate = 0.5f;
        private ProjectileFactory _projectileFactory;

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
            _projectileFactory = new ProjectileFactory(Instantiate(projectileFactoryConfiguration));
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
            _projectileFactory.Create(currentSelectedProjectileId, firingPoint.position, firingPoint.rotation, _team);
            // _projectileFactory.Create(testProjectileId, firingPoint.position, firingPoint.rotation);
            
            _isAbleToFire = false;
            StartCoroutine(FiringCooldownCoroutine());
        }
    }
}