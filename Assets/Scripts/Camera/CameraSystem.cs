using UnityEngine;
using System.Collections;

public class CameraSystem : MonoBehaviour
{
    Camera cam;
    float startingHeight;
    float height = 0;

    void Start()
    {
        cam = GetComponent<Camera>();
        startingHeight = cam.transform.position.y;
    }

    void Update()
    {
        DynamicHeight();
    }

    // adjust camera's height based on shape count
    void DynamicHeight()
    {
        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");
        height = startingHeight + (shapes.Length / 120f);
        cam.transform.position = new Vector3(cam.transform.position.x, height, cam.transform.position.z);
    }

    public static IEnumerator CameraShake(Camera camera, float duration, float magnitude)
    {
        Vector3 originalPos = camera.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration && Time.timeScale != 0)
        {
            float x = -0.02f * Random.Range(-2f, 2f) * magnitude;

            camera.transform.localPosition = new Vector3(x, originalPos.y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        camera.transform.localPosition = originalPos;
    }
}
