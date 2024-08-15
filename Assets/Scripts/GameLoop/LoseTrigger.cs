using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
            // reset panel side to 0
            other.gameObject.GetComponent<Shape>().ResetPanelSide();

            // restart the scene for now
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
