using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject loadingIndicator;
    [SerializeField] GameObject[] elementsToHide;
    [SerializeField] GameObject[] optionsElements;
    [SerializeField] GameObject title;

    void Start()
    {
        loadingIndicator.SetActive(false);
        PlayerPrefs.DeleteAll(); // remove with full release
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

    public async void ShowOptions()
    {
        foreach (GameObject element in elementsToHide)
        {
            element.SetActive(false);
        }
        loadingIndicator.SetActive(true);
        await Task.Delay(250); // await scene loading
        loadingIndicator.SetActive(false);
        foreach (GameObject element in optionsElements)
        {
            element.SetActive(true);
        }
        title.SetActive(false);
    }

    public async void HideOptions()
    {
        foreach (GameObject element in optionsElements)
        {
            element.SetActive(false);
        }
        title.SetActive(true);
        loadingIndicator.SetActive(true);
        await Task.Delay(250); // await scene loading
        loadingIndicator.SetActive(false);
        foreach (GameObject element in elementsToHide)
        {
            element.SetActive(true);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
