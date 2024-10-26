// CREDIT FOR IDEA - JIMMY VEGAS: https://www.youtube.com/watch?v=cqGq__JjhMM

using UnityEngine;

public class SkyBoxSettings : MonoBehaviour
{
    [Range(0.1f, 0.5f)]
    public float rotateSpeed = 0.1f;
    void Update()
    {
        RotateSkybox();
    }

    // rotate the skybox asynchronously (get it.. like multithreading... async... nevermind)
    void RotateSkybox()
    {
        // use unscale time to keep the rotation consistent even on speed up or slow down
        RenderSettings.skybox.SetFloat("_Rotation", Time.unscaledTime * rotateSpeed * 1.25f);
    }

    // on exit, reset the skybox rotation
    void OnDisable()
    {
        RenderSettings.skybox.SetFloat("_Rotation", 0);
    }
}