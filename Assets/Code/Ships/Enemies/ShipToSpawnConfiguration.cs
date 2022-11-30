using Code.Ships.Weapons;
using UnityEngine;

namespace Code.Ships.Enemies
{
    [CreateAssetMenu(menuName = "ShipGame/ShipToSpawnConfiguration")]
    public class ShipToSpawnConfiguration : ScriptableObject
    {
        [SerializeField] private ShipId shipId;
        [SerializeField] private ProjectileId projectileId;
        [SerializeField] private float fireRate;
        [SerializeField] private Vector3 speed;
        [SerializeField] private Vector3 position;
        
        public string ShipId => shipId.Value;
        public ProjectileId ProjectileId => projectileId;
        public float FireRate => fireRate;
        public Vector3 Speed => speed;
        public Vector3 Position => position;
    }
}