using UnityEngine;

public class CubeVisual : MonoBehaviour
{
    // if collides with another object
    private void OnTriggerEnter(Collider other)
    {
        // move up visual cube
        if (other.gameObject.tag != "Ground")
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + 1f, transform.position.y + 1f, 10), transform.position.z);
    }

    private void OnTriggerExit(Collider other)
    {
        // move down visual cube
        if (other.gameObject.tag != "Ground")
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - 1f, -1, 10), transform.position.z);
    }
}
