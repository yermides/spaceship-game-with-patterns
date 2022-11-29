using Code.Input;
using UnityEngine;

namespace Code.Ships
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private float speed = 5.0f;
        private Transform _transform;
        private Camera _camera;
        private IInputAdapter _inputAdapter;

        private void Awake()
        {
            _transform = transform;
            _camera = Camera.main;
        }

        private void Update()
        {
            var direction = GetDirection();
            Move(direction);
        }

        public void Set(IInputAdapter inputAdapter)
        {
            _inputAdapter = inputAdapter;
        }

        private void Move(Vector3 direction)
        {
            _transform.Translate(direction * (speed * Time.deltaTime));
            ClampFinalPosition();
            // _transform.position += movement;
        }

        private void ClampFinalPosition()
        {
            const float margin = 0.03f;
            
            var viewpoint = _camera.WorldToViewportPoint(_transform.position);
            viewpoint.x = Mathf.Clamp(viewpoint.x, margin, 1.0f - margin);
            viewpoint.y = Mathf.Clamp(viewpoint.y, margin, 1.0f - margin);
            _transform.position = _camera.ViewportToWorldPoint(viewpoint);
        }

        private Vector3 GetDirection()
        {
            return _inputAdapter.GetDirection();
        }
    }
}
