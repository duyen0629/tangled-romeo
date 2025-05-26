using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float rotationSpeed = 50f;
    public float mouseRotationSpeed = 5f;
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
            Debug.LogWarning("CameraControl must be attached to a Camera.");
        }
    }

    void Update()
    {
        bool isRightMouse = Input.GetMouseButton(1);
        bool isLeftMouse = Input.GetMouseButton(0);
        bool isAltHeld = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);

        if (isAltHeld && isLeftMouse)
        {
            HandleMouseRotation(); // ALT + LMB for orbit (Scene View style)
        }
        else if (isRightMouse)
        {
            HandleZoom();     // RMB + W/S
            HandleRotation(); // RMB + A/D
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

    void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Orbit horizontally around Y-axis
        transform.RotateAround(initialPosition, Vector3.up, mouseX * mouseRotationSpeed);

        // Orbit vertically around camera's local right axis
        transform.RotateAround(initialPosition, transform.right, -mouseY * mouseRotationSpeed);
    }
}
