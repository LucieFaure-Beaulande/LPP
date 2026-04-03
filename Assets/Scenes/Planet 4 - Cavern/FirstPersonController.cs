using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Look")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private Transform cameraTransform;

    private CharacterController _controller;
    private float _verticalRotation = 0f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
    }

    // Moves the player horizontally only using WASD/ZQSD
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        direction.y = 0f; // prevent any vertical drift

        _controller.Move(direction * moveSpeed * Time.deltaTime);
    }

    // Rotates camera vertically and player body horizontally using mouse
    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Horizontal: rotate the whole player body
        transform.Rotate(Vector3.up * mouseX);

        // Vertical: rotate only the camera, clamped to avoid flipping
        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }
}