using Code.Input;
using UnityEngine;

namespace Code.Ships
{
    public class ShipInstaller : MonoBehaviour
    {
        [SerializeField] private Ship shipReference;
        [SerializeField] private bool useUnityInput = true;

        private void Awake()
        {
            if (useUnityInput)
            {
                shipReference.Set(new UnityInputAdapter());
            }
            else
            {
                shipReference.Set(new AIInputAdapter(shipReference));
            }
        }
    }
}