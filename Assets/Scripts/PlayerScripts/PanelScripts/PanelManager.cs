using UnityEngine;
using TMPro;
using System.Threading.Tasks;
public class PanelManager : MonoBehaviour // panel manager is a game manager but for a player instance
{
    [SerializeField]
    float rotationRate = 1.0f;

    [HideInInspector] int score = 0;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject[] layers;

    Transform panelTransform;
    delegate void State();
    State currentState;
    private int rotatedAmount;
    public bool handlingClearedRow = false;

    void Start()
    {
        panelTransform = GetComponent<Transform>();
        currentState = RotateOnInput;
    }

    void Update()
    {
        currentState.Invoke();
    }

    void RotateOnInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentState = () => RotatePanel(new Vector3(0, -rotationRate, 0));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentState = () => RotatePanel(new Vector3(0, rotationRate, 0));
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
        }

        // if it hasnt rotated 90 degrees, rotate the panel
        panelTransform.Rotate(direction);
    }

    public async void UpdateScore()
    {
        for (int i = 0; i < 100; i++)
        {
            score++;
            scoreText.text = "Score " + score.ToString();
            await Task.Delay(5 * (int)(1 + Time.deltaTime));
        }
    }

    public async void HandleClearedRow(int layerAffectedIndex)
    {
        handlingClearedRow = true;
        await Task.Delay(1000); // wait for row to be cleared before moving pieces down

        // move all pieces in layers above the cleared row down
        for (int i = layerAffectedIndex + 1; i < layers.Length; i++)
        {
            layers[i].GetComponent<LayerManager>().MoveDown();
        }
        handlingClearedRow = false;
    }
}
