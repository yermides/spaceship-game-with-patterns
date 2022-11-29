using UnityEngine;

namespace Code.Input
{
    public class UnityInputAdapter : IInputAdapter
    {
        public Vector3 GetDirection()
        {
            return new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0, UnityEngine.Input.GetAxis("Vertical"));
        }
    }
}