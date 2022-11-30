using Code.Input;
using Code.Ships.CheckLimits;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Ships
{
    public enum ShipInputMethod
    {
        UseUnityInput,
        UseAIInput
    }

    public class ShipInstaller : MonoBehaviour
    {
        [FormerlySerializedAs("shipReference")] [SerializeField] private ShipMediator shipMediatorReference;
        [SerializeField] private ShipInputMethod shipInputMethod;

        private void Awake()
        {
            if (shipInputMethod == ShipInputMethod.UseUnityInput)
            {
                shipMediatorReference.Configure(
                    new UnityInputAdapter(), 
                    new ViewportLimitChecker(shipMediatorReference.transform));
            }
            else if(shipInputMethod == ShipInputMethod.UseAIInput)
            {
                shipMediatorReference.Configure(
                    new AIInputAdapter(shipMediatorReference),
                    new ViewportLimitChecker(shipMediatorReference.transform)
                    // new InitialPositionLimitChecker(shipMediatorReference.transform, 5.0f)
                    );
            }
        }
    }
}