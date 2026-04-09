using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadPlanet1Menu()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayPlanet1()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadPlanet2Menu()
    {
        SceneManager.LoadScene(3);
    }
    public void PlayPlanet2()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadPlanet3Menu()
    {
        SceneManager.LoadScene(5);
    }
    public void PlayPlanet3()
    {
        SceneManager.LoadScene(6);
    }
    public void LoadPlanet4Menu()
    {
        SceneManager.LoadScene(7);
    }
    public void PlayPlanet4()
    {
               SceneManager.LoadScene(8);
    }
}
