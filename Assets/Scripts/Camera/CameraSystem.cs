using UnityEngine;
using System.Collections;

public class CameraSystem : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // adjust camera's height based on shape count
        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");
        float height = 3.66f;
        height += shapes.Length / 90f;
        cam.transform.position = new Vector3(cam.transform.position.x, height, cam.transform.position.z);
    }

    public static IEnumerator CameraShake(Camera camera, float duration, float magnitude)
    {
        Vector3 originalPos = camera.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = -0.02f * UnityEngine.Random.Range(-2f, 2f) * magnitude;

            camera.transform.localPosition = new Vector3(x, originalPos.y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        camera.transform.localPosition = originalPos;
    }
}
