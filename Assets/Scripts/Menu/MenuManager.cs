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
        title.SetActive(false);
        optionsElements[1].SetActive(true);
    }

    public async void HideOptions()
    {
        optionsElements[1].SetActive(false);
        title.SetActive(true);
        loadingIndicator.SetActive(true);
        await Task.Delay(250); // await scene loading
        loadingIndicator.SetActive(false);
        foreach (GameObject element in elementsToHide)
        {
            element.SetActive(true);
        }
    }
    public void ShowControls()
    {
        title.SetActive(false);
        foreach (GameObject element in elementsToHide)
        {
            element.SetActive(false);
        }
        optionsElements[0].SetActive(true);
    }

    public void CloseControls()
    {
        title.SetActive(true);
        foreach (GameObject element in elementsToHide)
        {
            element.SetActive(true);
        }
        optionsElements[0].SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
