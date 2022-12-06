using System.Collections.Generic;
using Code.Ships.Weapons.Projectiles;
using NaughtyAttributes;
using UnityEngine;

namespace Code.Ships.Weapons
{
    [CreateAssetMenu(menuName = "ShipGame/ProjectileFactoryConfiguration")]
    public class ProjectileFactoryConfiguration : ScriptableObject
    {
        [SerializeField] private ProjectileBehaviour[] projectiles;
        private Dictionary<string, ProjectileBehaviour> _idToProjectileDictionary;
        
        [SerializeField] SerializedDictionary<ProjectileId, ProjectileBehaviour> idProjectileBehavioursDictionary;

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
        
        public ProjectileBehaviour GetProjectileById(ProjectileId id)
        {
            // TODO: handle the error if not found
            return idProjectileBehavioursDictionary[id];
        }
    }
}