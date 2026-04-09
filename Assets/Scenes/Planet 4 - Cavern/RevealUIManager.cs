using UnityEngine;

public class RevealUIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject firstButton;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject planet2Button;
    [SerializeField] private GameObject planet3Button;
    [SerializeField] private GameObject planet1Button;

    [SerializeField] private FirstPersonController firstPersonController;

    private void Start()
    {
        firstButton.SetActive(false);
        mainMenuButton.SetActive(false);
        planet2Button.SetActive(false);
        planet3Button.SetActive(false);
        planet1Button.SetActive(false);
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
        planet3Button.SetActive(true);
        planet1Button.SetActive(true);
    }
}