using System;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

namespace Gameplay
{
    // [Serializable] public class IntToProjectileDictionary : SerializableDictionaryBase<int, Projectile> { }
    [Serializable] public class IdToProjectileDictionary : SerializableDictionaryBase<ProjectileEnumId, Projectile> { }
    
    [CreateAssetMenu(fileName = "ProjectileFactoryConfiguration", menuName = "ScriptableObjects/ProjectileFactoryConfiguration", order = 1)]
    public class ProjectileFactoryConfiguration : ScriptableObject
    {
        [SerializeField] private IdToProjectileDictionary idToPrefabsDictionary;
        
        public Projectile GetPrefabById(ProjectileEnumId id)
        {
            idToPrefabsDictionary.TryGetValue(id, out Projectile projectile);
            return projectile;
        }
    }
}