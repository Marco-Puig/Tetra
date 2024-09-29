using UnityEngine;
using TMPro;

public class UIFlashFadeAnimation : MonoBehaviour
{
    [Range(0, 1f)]
    public float flashRate = 0.5f;
    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.PingPong(Time.time * flashRate, 0.8f));
    }
}
