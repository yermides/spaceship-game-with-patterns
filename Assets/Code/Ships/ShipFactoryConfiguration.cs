using System.Collections.Generic;
using UnityEngine;

namespace Code.Ships
{
    [CreateAssetMenu(menuName = "ShipGame/ShipFactoryConfiguration")]
    public class ShipFactoryConfiguration : ScriptableObject
    {
        [SerializeField] private ShipMediator[] ships;
        private Dictionary<string, ShipMediator> _idToShipDictionary;

        private void Awake()
        {
            _idToShipDictionary = new Dictionary<string, ShipMediator>();

            foreach (var ship in ships)
            {
                _idToShipDictionary.Add(ship.Id, ship);
            }
        }

        public ShipMediator GetProjectileById(string id)
        {
            // TODO: handle the error if not found
            return _idToShipDictionary[id];
        }
    }
}