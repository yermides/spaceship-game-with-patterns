using NaughtyAttributes;
using UnityEngine;
using MyLabel = NaughtyAttributes.LabelAttribute;

namespace Code.Ships
{
    [CreateAssetMenu(menuName = "ShipGame/ShipId")]
    public class ShipId : ScriptableObject
    {
        [SerializeField, Tag, MyLabel("Ship Id Value")] 
        private string id;
        
        public string Value => id;
    }
}