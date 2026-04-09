using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayPlanet1()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayPlanet2()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadPlanet2Menu()
    {
        SceneManager.LoadScene(2);
    }
    public void PlayPlanet3()
    {
        SceneManager.LoadScene(4);
    }
    public void PlayPlanet4()
    {
               SceneManager.LoadScene(5);
    }
}
