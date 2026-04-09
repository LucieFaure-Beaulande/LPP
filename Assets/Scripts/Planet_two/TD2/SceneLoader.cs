using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void PlayPlanet2()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadPlanet2Menu()
    {
        SceneManager.LoadScene(1);
    }
}
