using UnityEngine;

public class RevealUIManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonObject;
    [SerializeField] private FirstPersonController firstPersonController;

    private void Start()
    {
        if (buttonObject != null)
            buttonObject.SetActive(false);
    }

    public void ShowButtonAndEnableMouse()
    {
        if (buttonObject != null)
            buttonObject.SetActive(true);

        if (firstPersonController != null)
            firstPersonController.DisablePlayerControl();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}