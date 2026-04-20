using UnityEngine;

public class FishButton : MonoBehaviour
{
    public BoidManager boidManager; // Assign your BoidManager object here
    public GameObject uiPrompt;      // A UI Text/GameObject saying "Press E"

    private bool isPlayerNearby = false;

    void Start()
    {
        // Hide the prompt at the start
        if (uiPrompt != null) uiPrompt.SetActive(false);
    }

    void Update()
    {
        // If player is nearby and presses E, call the method in BoidManager
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            boidManager.addFish();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the Player
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (uiPrompt != null) uiPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (uiPrompt != null) uiPrompt.SetActive(false);
        }
    }
}