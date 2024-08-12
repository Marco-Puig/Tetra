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
    delegate void State();
    State currentState;
    private int rotatedAmount;
    GameObject fallingShape;

    void Start()
    {
        panelTransform = GetComponent<Transform>();
        currentState = RotateOnInput;
    }

    void Update()
    {
        currentState.Invoke();
        FindFallingShape();
    }

    void RotateOnInput()
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

    void FindFallingShape()
    {
        // find the falling cube
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Shape");

        foreach (GameObject cube in cubes)
        {
            // if cube isnt the main part of shape, skip
            if (cube.GetComponent<Shape>() == null)
                continue;

            if (cube.GetComponent<Shape>().currentState == cube.GetComponent<Shape>().DropCube)
            {
                fallingShape = cube;
                break;
            }
        }
    }

    // smooth rotation
    void RotatePanel(Vector3 direction)
    {
        // increment the rotation amount
        rotatedAmount += (int)rotationRate;

        // if it has rotated 90 degrees, reset the rotation amount and switch out of this state
        if (rotatedAmount >= 90)
        {
            rotatedAmount = 0;
            currentState = RotateOnInput;
            fallingShape.GetComponent<Shape>().HandleCubeSides((int)direction.y);  // handle cube sides
        }

        // if it hasnt rotated 90 degrees, rotate the panel
        panelTransform.Rotate(direction);
    }

    public async void UpdateScore()
    {
        // score count up animation
        for (int i = 0; i < 100; i++)
        {
            score++;
            scoreText.text = "Score " + score.ToString();
            await Task.Delay(5 * (int)(1 + Time.deltaTime));
        }
    }

    public void HandleClearedRow(int layerAffectedIndex)
    {
        // move all pieces in layers above the cleared row down
        for (int i = layerAffectedIndex + 1; i < layers.Length; i++)
        {
            layers[i].GetComponent<LayerManager>().MoveDownPieces();
        }
    }
}
