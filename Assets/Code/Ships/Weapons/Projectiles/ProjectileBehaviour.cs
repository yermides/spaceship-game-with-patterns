using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

namespace Code.Ships.Weapons.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField, Expandable] 
        protected ProjectileId id;
        
        [SerializeField] protected Rigidbody rigidbodyReference;
        protected Transform myTransform;

        public string Id => id.Value;

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
    }
}