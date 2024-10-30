using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseTrigger : MonoBehaviour
{
    [SerializeField] AudioClip soundEffect;
    AudioManager audioManager;

    void OnTriggerEnter(Collider other)
    {
        if (CheckShapeInside(other))
        {
            // better to do GameManger.Instance.GameState = GameState.Lose;
            LoseGame();
        }
    }

    bool CheckShapeInside(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
            // check if the shape/piece is not in the drop state if it is the child of a shape
            if (other.gameObject.GetComponent<Shape>() == null)
            {
                // check if the shape is not in the drop state
                if (other.gameObject.transform.parent.GetComponent<Shape>().currentState == other.gameObject.transform.parent.GetComponent<Shape>().DropShape)
                {
                    return false;
                }

                // reset panel side to 0
                other.gameObject.transform.parent.GetComponent<Shape>().ResetPanelSide();
            }
            // check if the shape is not in the drop state if it is the main shape
            else
            {
                if (other.gameObject.GetComponent<Shape>().currentState == other.gameObject.GetComponent<Shape>().DropShape)
                {
                    return false;
                }

                // reset panel side to 0
                other.gameObject.GetComponent<Shape>().ResetPanelSide();
            }

            // if shape is inside the trigger and not in the drop state
            return true;
        }

        // if no shape is inside the trigger
        return false;
    }

    async void LoseGame()
    {
        // stop spawner
        GameObject.Find("Spawner").GetComponent<Spawner>().StopSpawning();

        // get all the shapes in the scene
        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");

        // slowly break all the shapes for a cool effect
        foreach (GameObject shape in shapes)
        {
            // break the shape
            Destroy(shape);
            await Task.Delay(50);
        }

        // then restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void PlaySFX()
    {
        audioManager.PlaySound(soundEffect); // better idea to have a sorted list of sound effects to pick from
    }
}
