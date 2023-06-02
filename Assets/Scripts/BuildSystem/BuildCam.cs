using UnityEngine;

public class BuildCam : MonoBehaviour
{

    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float yawSpeed = 1f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 20f;
    public Vector3 targetOffset = Vector3.zero;

    [SerializeField] private float yaw = 0f;
    [SerializeField] private float pitch = 0f;
    [SerializeField] private float distance = 5f;

    private void Update()
    {
        // Rotate the camera with A and D keys
        float rotation = Input.GetAxis("Horizontal") * -sensitivity;
        yaw += rotation;

        // Store the current distance from the central point
        float currentDistance = Vector3.Distance(transform.position, targetOffset);

        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance - zoom, minDistance, maxDistance);

        // Adjust the pitch while remaining at the same distance
        float pitchZoom = Input.GetAxis("Vertical") * yawSpeed;
        pitch = Mathf.Clamp(pitch + pitchZoom, 0, 85f);

        // Calculate the new position of the camera based on its rotation and distance from the central point
        float radYaw = Mathf.Deg2Rad * yaw;
        float radPitch = Mathf.Deg2Rad * pitch;
        float x = distance * Mathf.Sin(radYaw) * Mathf.Cos(radPitch);
        float y = distance * Mathf.Sin(radPitch);
        float z = distance * Mathf.Cos(radYaw) * Mathf.Cos(radPitch);

        Vector3 position = new Vector3(x, y, z) + targetOffset;
        transform.position = position;

        // Look at the central point
        transform.LookAt(targetOffset);
    }
}