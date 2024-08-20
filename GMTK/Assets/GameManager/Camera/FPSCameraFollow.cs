using UnityEngine;

public class FPSCameraFollow : MonoBehaviour
{
    public float rotationSpeed = 2f; // Speed of rotation following the mouse movement
    public float maxOffsetX = 15f; // Maximum offset from the initial rotation around the X axis (up/down)
    public float maxOffsetY = 15f; // Maximum offset from the initial rotation around the Y axis (left/right)

    private float rotationX = 0f; // Accumulated rotation offset around the X axis
    private float rotationY = 0f; // Accumulated rotation offset around the Y axis
    private Quaternion initialRotation; // Initial rotation of the camera


    bool zoom;

    float targetZoom;
    Quaternion targetRot;


    void Start()
    {
        // Store the initial rotation of the camera
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        // Get the mouse delta (mouse movement since the last frame)
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Accumulate rotation based on mouse movement
        rotationY += mouseX * rotationSpeed;
        rotationX -= mouseY * rotationSpeed;

        // Clamp the rotation offsets relative to the initial rotation
        rotationX = Mathf.Clamp(rotationX, -maxOffsetX, maxOffsetX);
        rotationY = Mathf.Clamp(rotationY, -maxOffsetY, maxOffsetY);

        // Calculate the target rotation based on the initial rotation and the accumulated offsets
        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);

        // Apply the rotation relative to the initial rotation
        

        if(zoom)
        {
            targetZoom = 55;
            targetRot = Quaternion.Euler(new Vector3(1,0,0)) * targetRotation;
        }
        else
        {
            targetZoom = 62;
            targetRot = initialRotation * targetRotation;
        }

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetZoom, 20 * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.rotation, targetRot, 20f * Time.deltaTime);
    }

    public void ToggleZoom()
    {
        zoom = !zoom;
    }
}

