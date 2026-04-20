using UnityEngine;

public class DoorPassageTrigger : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;

    private static string btnName;

    DoorPassageTrigger()
    {
        btnName = "DoorPassageTrigger";
    }
    public static void setBtnName(string name)
    {
        btnName = name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && btnName == "planet1")
        {
            sceneLoader.PlayPlanet1();
        }
        else if (other.CompareTag("Player") && btnName == "planet2")
        {
            sceneLoader.PlayPlanet2();
        }
        else if (other.CompareTag("Player") && btnName == "planet3")
        {
            sceneLoader.PlayPlanet3();
        }
        else
        {
            Debug.LogWarning("Player entered the trigger, but btnName is not set to a valid value.");
        }
    }
}