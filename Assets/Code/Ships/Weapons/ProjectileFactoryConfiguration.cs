using System.Collections.Generic;
using Code.Ships.Weapons.Projectiles;
using UnityEngine;

namespace Code.Ships.Weapons
{
    [CreateAssetMenu]
    public class ProjectileFactoryConfiguration : ScriptableObject
    {
        [SerializeField] private ProjectileBehaviour[] projectiles;
        private Dictionary<string, ProjectileBehaviour> _idToProjectileDictionary;

        private void Awake()
        {
            _idToProjectileDictionary = new Dictionary<string, ProjectileBehaviour>();

            foreach (var projectile in projectiles)
            {
                _idToProjectileDictionary.Add(projectile.Id, projectile);
            }
        }

        public ProjectileBehaviour GetProjectileById(string id)
        {
            // TODO: handle the error if not found
            return _idToProjectileDictionary[id];
        }
    }
}