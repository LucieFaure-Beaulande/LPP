using UnityEngine;

public class ButtonInteractionZone : MonoBehaviour
{
    [SerializeField] private GameObject buttonObject;
    [SerializeField] private FirstPersonController firstPersonController;

    private void Start()
    {
        if (buttonObject != null)
            buttonObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (buttonObject != null)
            buttonObject.SetActive(true);
        if (firstPersonController != null)
            firstPersonController.EnableMovementOnly();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (firstPersonController != null)
            firstPersonController.EnablePlayerControl();
    }
}