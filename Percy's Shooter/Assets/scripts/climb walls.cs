using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbwalls : MonoBehaviour
{
    private int vaultLayer;
    public Camera cam;
    private float playerHeight = 2f;
    private float playerRadius = 0.5f;
    public float one = 1f;
    public float two = 0.6f;
    public float three = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        vaultLayer = LayerMask.NameToLayer("Climb");
        vaultLayer = ~vaultLayer;
    }

    // Update is called once per frame
    void Update()
    {
        Vault();
    }
    public void Vault()
    { 
    if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out var firstHit, one, vaultLayer))
            {
                Debug.Log("1");
                if(Physics.Raycast(firstHit.point + (cam.transform.forward * playerRadius) + (Vector3.up * two * playerHeight), Vector3.down, out var secondHit, playerHeight))
                {
                    Debug.Log("2");
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
