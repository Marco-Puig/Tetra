using UnityEngine;
using TMPro;

public class DisplayBestScore : MonoBehaviour
{
    void Start()
    {
        int bestScore = PlayerPrefs.GetInt("Best Score", 0);
        GetComponent<TMP_Text>().text = "Best Score: " + bestScore.ToString();
    }
}
