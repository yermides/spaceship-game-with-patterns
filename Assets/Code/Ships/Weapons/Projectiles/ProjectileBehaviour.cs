using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Code.Ships.Common;
using NaughtyAttributes;
using UnityEngine;

namespace Code.Ships.Weapons.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class ProjectileBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] 
        protected ProjectileId id;
        
        [SerializeField] 
        protected Rigidbody rigidbodyReference;
        protected Transform myTransform;

        [ShowNonSerializedField]
        private Teams _team;

        public event Action<ProjectileBehaviour> onDestroyCallback;

        public string Id => id.Value;

        public void Configure(Teams team)
        {
            _team = team;
        }

        protected abstract void DoStart();
        protected abstract void DoMove();
        protected abstract void DoDestroy();

        private void Start()
        {
            myTransform = transform;
            DoStart();
            StartCoroutine(DestroyInSecondsCoroutine());
        }

        private void FixedUpdate()
        {
            DoMove();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            if (Team == damageable.Team) return;

            damageable.TakeDamage(1);
            DestroyProjectile();
        }

        private IEnumerator DestroyInSecondsCoroutine(float seconds = 3.0f)
        {
            yield return new WaitForSeconds(seconds);
            DestroyProjectile();
        }

        private void DestroyProjectile()
        {
            DoDestroy();
            Destroy(gameObject);
        }

        public void TakeDamage(int amount)
        {
            DestroyProjectile();
        }

        private void OnDestroy()
        {
            onDestroyCallback?.Invoke(this);
            onDestroyCallback = null;
        }

        public Teams Team => _team;
    }
}