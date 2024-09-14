using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("DevScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
