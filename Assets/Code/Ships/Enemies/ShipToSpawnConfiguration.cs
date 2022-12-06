using Code.Ships.Common;
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
        [SerializeField, Min(1)] private int health = 1;
        [SerializeField, Min(1)] private int score = 1;
        // [SerializeField] private Teams team;
        
        public string ShipId => shipId.Value;
        public ProjectileId ProjectileId => projectileId;
        public float FireRate => fireRate;
        public Vector3 Speed => speed;
        public Vector3 Position => position;

        public int Health => health;

        public int Score => score;
        // public Teams Team => team;
    }
}