using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 5f; // Speed of rotation in degrees per second

    private void Update()
    {
        // Rotate the skybox over time
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
