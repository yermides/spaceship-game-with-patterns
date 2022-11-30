using UnityEngine;
using NaughtyAttributes;

namespace Code.Ships.Weapons
{
    [CreateAssetMenu]
    public class ProjectileId : ScriptableObject
    {
        [SerializeField, Tag, Label("Projectile Id Value")] 
        private string id;
        
        public string Value => id;
    }

}