using UnityEngine;

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
}
