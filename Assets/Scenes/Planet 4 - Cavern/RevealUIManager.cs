using UnityEngine;

public class RevealUIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject firstButton;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject planet2Button;

    [SerializeField] private FirstPersonController firstPersonController;

    private void Start()
    {
        firstButton.SetActive(false);
        mainMenuButton.SetActive(false);
        planet2Button.SetActive(false);
    }

    public void ShowFirstButton()
    {
        firstButton.SetActive(true);

        if (firstPersonController != null)
            firstPersonController.EnableMovementOnly();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowChoiceButtons()
    {
        firstButton.SetActive(false);

        mainMenuButton.SetActive(true);
        planet2Button.SetActive(true);
    }
}