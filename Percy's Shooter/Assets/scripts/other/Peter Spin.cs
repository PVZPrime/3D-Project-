using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float rotationSpeed = 50f; // Rotation speed in degrees per second
    public Vector3 rotationAxis = new Vector3(0, 1, 0); // Default to rotate around the Y-axis

    void Update()
    {
        // Rotate the object based on the rotation speed and axis
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
