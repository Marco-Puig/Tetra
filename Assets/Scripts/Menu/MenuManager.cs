using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject loadingIndicator;
    [SerializeField] GameObject[] elementsToHide;

    void Start()
    {
        loadingIndicator.SetActive(false);
    }
    public async void StartGame()
    {
        // hide buttons and show loading indicator
        foreach (GameObject element in elementsToHide)
        {
            element.SetActive(false);
        }
        loadingIndicator.SetActive(true);
        await Task.Delay(1000); // await scene loading
        SceneManager.LoadScene("DevScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
