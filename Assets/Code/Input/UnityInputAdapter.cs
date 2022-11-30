using UnityEngine;

namespace Code.Input
{
    public class UnityInputAdapter : IInputAdapter
    {
        private readonly KeyCode _firingKey;

        public UnityInputAdapter(KeyCode firingKey = KeyCode.Space)
        {
            _firingKey = firingKey;
        }
        
        public Vector3 GetDirection()
        {
            return new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0, UnityEngine.Input.GetAxis("Vertical"));
        }

        public bool DidRequestToFire()
        {
            return UnityEngine.Input.GetKey(_firingKey);
        }
    }
}