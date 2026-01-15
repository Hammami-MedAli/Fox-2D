using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void Exit()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.StopMusic();
        }
        SceneManager.LoadScene(0,LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    public void Next()
    {
        int current = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(current + 1);
        Time.timeScale = 1;
    }
}
