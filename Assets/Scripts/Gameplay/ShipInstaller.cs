using UnityEngine;

namespace Gameplay
{
    public class ShipInstaller : MonoBehaviour
    {
        [SerializeField] private Ship ship;
        [SerializeField] private bool controlledByAI;
        [SerializeField] private bool constrainMovement;
        
        private void Awake()
        {
            ship.Configure(GetInputReceiver());
            ship.Configure(GetMovementConstrainer());
        }

        private IInputReceiver GetInputReceiver()
        {
            IInputReceiver inputReceiver;
            
            if (controlledByAI)
            {
                inputReceiver = new AIInputAdapter(ship);
            }
            else
            {
                // Controlled by Input
                inputReceiver = new UnityInputAdapter();
            }
            
            // If we had a joystick, we could create a JoystickInputAdapter instead, #if UNITY_ANDROID #else or smth

            return inputReceiver;
        }

        private IMovementConstrainer GetMovementConstrainer()
        {
            IMovementConstrainer movementConstrainer;
            
            if (constrainMovement)
            {
                movementConstrainer = new ForbidOutOfBoundsStrategy();
            }
            else
            {
                movementConstrainer = new FreeMovementStrategy();
            }

            return movementConstrainer;
        }
    }
}