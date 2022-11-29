using System;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

namespace Gameplay
{
    [Serializable] public class IdToShipModelDictionary : SerializableDictionaryBase<ShipModelEnumId, GameObject> { }
    
    [CreateAssetMenu(fileName = "ShipModelFactoryConfiguration", menuName = "ScriptableObjects/ShipModelFactoryConfiguration", order = 1)]
    public class ShipModelFactoryConfiguration : ScriptableObject
    {
        [SerializeField] private IdToShipModelDictionary idToPrefabsDictionary;
        
        public GameObject GetPrefabById(ShipModelEnumId id)
        {
            idToPrefabsDictionary.TryGetValue(id, out GameObject meshGameObject);
            return meshGameObject;
        }
    }
}