using UnityEngine;
using UnityEngine.UI;

public class FPSCap : MonoBehaviour
{
    // Script that sets the FPS of the game via a debug ui slider
    [SerializeField] Slider slider;
    void Update()
    {
        Application.targetFrameRate = (int)slider.value;
    }
}
