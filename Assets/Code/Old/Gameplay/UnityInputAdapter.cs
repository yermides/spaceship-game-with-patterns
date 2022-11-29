using UnityEngine;

namespace Gameplay
{
    public class UnityInputAdapter : IInputReceiver
    {
        // private float timeBetweenFiring = 0.2f;
        public UnityInputAdapter()
        {
        }

        public Vector2 GetDirection()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        public bool GetRequestForFiring()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}