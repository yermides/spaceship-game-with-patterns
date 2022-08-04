using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class FreewayCameraMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 8.0f;
        [SerializeField] private float sensibility = 300.0f;
        [SerializeField] private float smoothedFactor = 2.0f;

        Vector3 desiredPosition, desiredRotation;

        private bool isUpdating = false;

        private void Start()
        {
            StartCoroutine(EnableUpdatingCoroutine());
        }

        private void LateUpdate()
        {
            if (!isUpdating) return;

            float deltatime = Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.LogWarning(deltatime);
            }

            // float deltatime = Time.smoothDeltaTime;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            desiredPosition = transform.position;
            desiredPosition += transform.forward * (vertical * speed * deltatime);
            desiredPosition += transform.right * (horizontal * speed * deltatime);

            // Movement forward/backwards
            // transform.Translate(Vector3.forward * (vertical * speed * deltatime));

            // Move to the sides
            // transform.Translate(Vector3.right * (horizontal * speed * deltatime));

            // Move up/down

            if (Input.GetKey(KeyCode.Space))
            {
                desiredPosition += Vector3.up * (speed * deltatime);
                // transform.position += Vector3.up * (speed * deltatime);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                desiredPosition += Vector3.down * (speed * deltatime);
                // transform.position += Vector3.down * (speed * deltatime);
            }

            // Rotate with mouse delta movement
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 rotationEulerAngles = new Vector3(
                mouseY * sensibility * deltatime * -1.0f
                , mouseX * sensibility * deltatime
                , 0
            );

            desiredRotation = transform.eulerAngles + rotationEulerAngles;

            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothedFactor * deltatime);
            // transform.eulerAngles = desiredRotation;

            // transform.eulerAngles += rotationEulerAngles;
            // Clamp
            //float clampedPitch = Mathf.Clamp(transform.eulerAngles.x, -85.0f, 85.0f);
            //transform.eulerAngles = new Vector3(clampedPitch, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        private IEnumerator EnableUpdatingCoroutine()
        {
            yield return new WaitForSeconds(0.4f);
            isUpdating = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}