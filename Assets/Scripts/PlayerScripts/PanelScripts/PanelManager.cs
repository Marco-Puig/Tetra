using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class PanelManager : MonoBehaviour // panel manager is a game manager but for a player instance
{
    [Header("Panel Manager")]
    [SerializeField] float rotationRate = 120.0f;

    [HideInInspector] int score = 0;
    [SerializeField] TMP_Text scoreText;   

    [Header("Dependencies")] 
    [SerializeField] ShapeMobileInput shapeMobileInput;

    Transform panelTransform;
    public delegate void State();
    public State currentState; // binary state machine - either rotating or not rotating

    private float rotatedAmount;
    GameObject fallingShape;

    public static PanelManager instance;

    private void Start()
    {
        // singleton pattern - manger should be unique
        if (instance == null)
        {
            instance = this;
        }

        panelTransform = GetComponent<Transform>();
        currentState = RotateOnInput;
    }

    private void Update()
    {
        currentState.Invoke();
        FindFallingShape();
    }

    private void FindFallingShape()
    {
        // find the falling cube
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Shape");

        foreach (GameObject cube in cubes)
        {
            // if cube isnt the main part of shape, skip
            if (cube.GetComponent<Shape>() == null)
                continue;

            if (cube.GetComponent<Shape>().currentState == cube.GetComponent<Shape>().DropShape)
            {
                fallingShape = cube;
                break;
            }
        }
    }

    public void RotateOnInput()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            currentState = () => RotatePanel(Vector3.down);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            currentState = () => RotatePanel(Vector3.up);
        }
    }

    // smooth rotation
    public void RotatePanel(Vector3 direction)
    {
        // disable controls while rotating
        shapeMobileInput.DisableMobileInput();
        // increment the rotation amount
        rotatedAmount += Mathf.Abs(direction.y * Time.deltaTime * rotationRate);

        // if it has rotated 90 degrees, reset the rotation amount and switch out of this state
        if ((int)rotatedAmount >= 90) // <1% error margin
        {
            rotatedAmount = 0;
            currentState = RotateOnInput;
            fallingShape.GetComponent<Shape>().HandleShapeSides((int)direction.y);  // handle shape sides
            shapeMobileInput.EnableMobileInput(); // re-enable controls
        }

        // if it hasnt rotated 90 degrees, rotate the panel
        panelTransform.Rotate(direction * Time.deltaTime * rotationRate);
    }

    public async void UpdateScore(int totalScore = 2000)
    {
        // score count up animation
        for (int i = 0; i < totalScore; i++)
        {
            score++;
            //scoreText.text = "Score " + score.ToString();
            scoreText.text = score.ToString();
            await Task.Delay(1);
        }

        // update best score if reached a new high score
        if (score > PlayerPrefs.GetInt("Best Score", 0))
        {
            PlayerPrefs.SetInt("Best Score", score);
            // Leaderboard Logic here - use MongoDB
            // https://www.mongodb.com/developer/code-examples/csharp/saving-data-in-unity3d-using-sqlite/
        }

    }
}
