using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewMovment
{
    public class PlayerCam : MonoBehaviour
    {
        public float sensX;
        public float sensY;
        public float MaxXRotationUp = 90f;
        public float MaxXRotationDown = -90f;

        public Transform orientatiion;

        float xRotation;
        float yRotation;
        private StarterAssetsInputs _input;

        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void Update()
        {
            float mouseX = -_input.look.x * Time.deltaTime * sensX;
            float mouseY = _input.look.y * Time.deltaTime * sensY;

            yRotation += mouseX;

            xRotation += mouseY;
            xRotation = Mathf.Clamp(xRotation, MaxXRotationDown, MaxXRotationUp);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientatiion.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}