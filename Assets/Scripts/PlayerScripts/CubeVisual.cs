using UnityEngine;

public class CubeVisual : MonoBehaviour
{
    // if collides with another object
    private void OnTriggerEnter(Collider other)
    {
        // move up visual cube
        transform.position += new Vector3(0, 1, 0);
    }
}
