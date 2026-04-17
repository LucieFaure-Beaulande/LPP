using UnityEngine;

public class DoorPassageTrigger : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            sceneLoader.PlayPlanet1();
    }
}