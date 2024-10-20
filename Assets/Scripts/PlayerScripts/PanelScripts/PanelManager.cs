using UnityEngine;
using TMPro;
using System.Threading.Tasks;
public class PanelManager : MonoBehaviour // panel manager is a game manager but for a player instance
{
    [Header("Panel Manager")]
    [SerializeField]
    float rotationRate = 1.0f;

    [HideInInspector] int score = 0;
    [SerializeField] TMP_Text scoreText;

    [Header("Layer Manager")]
    [SerializeField] GameObject[] layers;

    Transform panelTransform;
    public delegate void State();
    public State currentState;
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
        else
        {
            Destroy(gameObject);
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
            currentState = () => RotatePanel(new Vector3(0, -rotationRate, 0));
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            currentState = () => RotatePanel(new Vector3(0, rotationRate, 0));
        }
    }

    // smooth rotation
    public void RotatePanel(Vector3 direction)
    {
        // increment the rotation amount
        rotatedAmount = rotatedAmount + rotationRate; //* Time.deltaTime; <-- todo: fix rotation rate

        // if it has rotated 90 degrees, reset the rotation amount and switch out of this state
        if (rotatedAmount >= 90)
        {
            rotatedAmount = 0;
            currentState = RotateOnInput;
            fallingShape.GetComponent<Shape>().HandleShapeSides((int)direction.y);  // handle shape sides
        }

        // if it hasnt rotated 90 degrees, rotate the panel
        panelTransform.Rotate(direction);
    }

    public async void UpdateScore(int totalScore = 2000)
    {
        // score count up animation
        for (int i = 0; i < totalScore; i++)
        {
            score++;
            scoreText.text = "Score " + score.ToString();
            await Task.Delay((int)(1 + Time.deltaTime));
        }
    }
}
