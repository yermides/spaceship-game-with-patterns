using UnityEngine;

namespace Code.Level
{
    // Try to work as a parallax effect
    public class StreamableTerrain : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Vector3 _initialPosition;
        private static readonly Vector3 Direction = Vector3.back;
        private Transform _transform;
        private const float Boundaries = 80.0f;

        private void Awake()
        {
            _transform = transform;
            _initialPosition = _transform.position;
        }

        private void Update()
        {
            _transform.Translate(Direction * (speed * Time.deltaTime));

            if (Mathf.Abs(_transform.position.z - _initialPosition.z) >= (Boundaries))
            {
                _transform.Translate(-Direction * (Boundaries));
            }
        }

        private void Reset()
        {
            _transform.position = _initialPosition;
        }
    }
}