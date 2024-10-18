using UnityEngine;

public class Options : MonoBehaviour
{
    // windowed mode
    public void isFullscreen(bool toggle)
    {
        Screen.fullScreen = !toggle;
    }

    // set framerate
    public void setFramerate(int framerate)
    {
        QualitySettings.vSyncCount = 0;
        switch (framerate)
        {
            case 0:
                Application.targetFrameRate = -1;
                break;
            case 1:
                Application.targetFrameRate = 120;
                break;
            case 2:
                Application.targetFrameRate = 60;
                break;
            case 3:
                Application.targetFrameRate = 30;
                break;
            default:
                Application.targetFrameRate = 60;
                break;
        }
    }

}
