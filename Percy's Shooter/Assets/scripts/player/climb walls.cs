using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace player
{
    //add a delay befor pulling up and add a way to interact with animations
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class climbwalls : MonoBehaviour
    {
        private StarterAssetsInputs _input;
        public Camera cam;
        private float playerHeight = 2f;
        private float playerRadius = 0.5f;
        public float one = 1f;
        public float two = 0.6f;
        public float three = 0.5f;
        public LayerMask LayerMask;
        // Start is called before the first frame update
        void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();
        }

        // Update is called once per frame
        void Update()
        {
            Vault();
        }
        public void Vault()
        {
            if (_input.Climb)
            {
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out var firstHit, one, LayerMask))
                {
                    if (Physics.Raycast(firstHit.point + (cam.transform.forward * playerRadius) + (Vector3.up * two * playerHeight), Vector3.down, out var secondHit, playerHeight))
                    {
                        StartCoroutine(LerpVault(secondHit.point, three));
                    }
                }
            }

        }
        IEnumerator LerpVault(Vector3 TargetPosition, float duration)
        {
            float time = 0;
            Vector3 startPositon = transform.position;
            while (time < duration)
            {
                transform.position = Vector3.Lerp(startPositon, TargetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            transform.position = TargetPosition;
        }

    }
}