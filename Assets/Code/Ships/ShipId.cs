using NaughtyAttributes;
using UnityEngine;

namespace Code.Ships
{
    [CreateAssetMenu(menuName = "ShipGame/ShipId")]
    public class ShipId : ScriptableObject
    {
        [SerializeField, Tag, Label("Ship Id Value")] 
        private string id;
        
        public string Value => id;
    }
}