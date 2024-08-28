using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
            // check if the shape/piece is not in the drop state if it is the child of a shape
            if (other.gameObject.GetComponent<Shape>() == null)
            {
                // check if the shape is not in the drop state
                if (other.gameObject.transform.parent.GetComponent<Shape>().currentState == other.gameObject.transform.parent.GetComponent<Shape>().DropShape)
                {
                    return;
                }

                // reset panel side to 0
                other.gameObject.transform.parent.GetComponent<Shape>().ResetPanelSide();
            }
            // check if the shape is not in the drop state if it is the main shape
            else
            {
                if (other.gameObject.GetComponent<Shape>().currentState == other.gameObject.GetComponent<Shape>().DropShape)
                {
                    return;
                }

                // reset panel side to 0
                other.gameObject.GetComponent<Shape>().ResetPanelSide();
            }

            // restart the scene for now
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
