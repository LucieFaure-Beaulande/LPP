using UnityEngine;
using UnityEngine.UI; // Required to access the Button component

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Button))]
public class MuseumExitTrigger : MonoBehaviour
{
    public SceneLoader loader;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the collider is the player
        if (other.CompareTag("Player"))
        {
            // Simulate a click on the button
            loader.LoadPlanet2Menu();
            Debug.Log("Player entered portal trigger. Button invoked!");
        }
    }
}
