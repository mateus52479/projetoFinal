using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void activeconfig(GameObject go)
    {
        go.SetActive(true);
    }
    public void desactiveconfig(GameObject go)
    {
        go.SetActive(false);
    }
    public void activepause(GameObject go)
    {
        Time.timeScale = 0;
        go.SetActive(true);
    }
    public void desactivepause(GameObject go)
    {
        Time.timeScale = 1;
        go.SetActive(false);
    }
    public void voltarmenu(GameObject go)
    {
        SceneManager.LoadScene(0);
    }
}



