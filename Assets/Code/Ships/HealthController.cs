using Code.Ships.Common;
using NaughtyAttributes;
using UnityEngine;

namespace Code.Ships
{
    public class HealthController : MonoBehaviour, IDamageable
    {
        private IShip _ship;
        
        [ShowNonSerializedField] 
        private int _health = 1;

        [ShowNonSerializedField] 
        private Teams _team;

        public void Configure(IShip ship, int healthValue, Teams team)
        {
            _ship = ship;
            _health = healthValue;
            _team = team;
        }

        public void TakeDamage(int amount)
        {
            _health = Mathf.Max(0, _health - amount);
            var hasDied = _health <= 0;
            _ship.OnDamageReceived(hasDied);
        }

        public Teams Team => _team;
    }
}