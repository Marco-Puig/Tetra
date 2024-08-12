// CREDIT FOR IDEA - JIMMY VEGAS: https://www.youtube.com/watch?v=cqGq__JjhMM

using UnityEngine;

public class SkyBoxSettings : MonoBehaviour
{
    [Range(0.1f, 0.5f)]
    public float rotateSpeed = 0.1f;
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
    }

    // on exit, reset the skybox rotation
    void OnDisable()
    {
        RenderSettings.skybox.SetFloat("_Rotation", 0);
    }
}