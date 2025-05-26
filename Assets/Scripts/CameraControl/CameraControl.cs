using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float rotationSpeed = 50f;
    public float minZoom = 5f;
    public float maxZoom = 100f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Camera cam;

    void Start()
    {
        // Save initial transform state
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogWarning("CameraController must be attached to a Camera.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(1)) // Right Mouse Button is held
        {
            HandleZoom();
            HandleRotation();
        }
    }

    void HandleZoom()
    {
        float scroll = 0f;

        if (Input.GetKey(KeyCode.W))
            scroll = 1f;
        else if (Input.GetKey(KeyCode.S))
            scroll = -1f;

        transform.position += transform.forward * scroll * zoomSpeed * Time.deltaTime;

        // Clamp the distance from the initial position
        float distance = Vector3.Distance(initialPosition, transform.position);
        if (distance < minZoom)
            transform.position = initialPosition + (transform.forward * minZoom);
        else if (distance > maxZoom)
            transform.position = initialPosition + (transform.forward * maxZoom);
    }

    void HandleRotation()
    {
        float horizontal = 0f;

        if (Input.GetKey(KeyCode.D))
            horizontal = 1f;
        else if (Input.GetKey(KeyCode.A))
            horizontal = -1f;

        transform.RotateAround(initialPosition, Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
    }
}
