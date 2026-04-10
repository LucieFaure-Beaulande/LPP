using UnityEngine;
using UnityEngine.UI; // Required to access the Button component

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Button))]
public class TriggerButton : MonoBehaviour
{
    private Button portalButton;

    void Start()
    {
        // Grab the button component attached to this portal object
        portalButton = GetComponent<Button>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the collider is the player
        if (other.CompareTag("Player"))
        {
            // Simulate a click on the button
            portalButton.onClick.Invoke();
            Debug.Log("Player entered portal trigger. Button invoked!");
        }
    }
}