using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject mainUI;
    bool isPaused = false;

    void Update()
    {
        PauseGameButton();
    }

    void PauseGameButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused) PauseGame();
        else ResumeGame();
    }

    void PauseGame()
    {
        isPaused = true;
        mainUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPaused = false;
        mainUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }

}