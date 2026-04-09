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
    private bool _canLook = true;
    private bool _canMove = true;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        LockMouse();
    }

    private void Update()
    {
        if (_canMove)
            HandleMovement();

        if (_canLook)
            HandleLook();
    }

    public void EnablePlayerControl()
    {
        _canMove = true;
        _canLook = true;
        LockMouse();
    }

    public void DisablePlayerControl()
    {
        _canMove = false;
        _canLook = false;
        UnlockMouse();
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        direction.y = 0f;

        _controller.Move(direction * moveSpeed * Time.deltaTime);
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }
}