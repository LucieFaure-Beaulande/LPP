using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public GlobalSceneLoader scriptDeChargement;

    void OnMouseDown()
    {
        Debug.Log("Porte cliquée");
        scriptDeChargement.LoadScene("Planet 2 - Menu");
    }
}
