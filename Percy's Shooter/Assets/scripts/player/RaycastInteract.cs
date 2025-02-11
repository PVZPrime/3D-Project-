using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace player
{
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class RaycastInteract : MonoBehaviour
    {
        private StarterAssetsInputs _input;

        [SerializeField]
        float interactRange = 4;
        [SerializeField]
        TextMeshProUGUI InteractText;
        // Start is called before the first frame update
        void Start()
        {
            InteractText.enabled = false;
            _input = GetComponent<StarterAssetsInputs>();
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, interactRange))
            {
                if (hit.collider.gameObject.tag == "Interactable")
                {
                    InteractText.enabled = true;
                    if (_input.interact)
                    {
                        //needs a small cooldown
                        hit.collider.gameObject.GetComponent<Animator>().SetTrigger("Interact");
                        InteractText.enabled = false;
                    }
                }
                else
                {
                    InteractText.enabled = false;
                }
            }
            else
            {
                InteractText.enabled = false;
            }
        }
    }
}