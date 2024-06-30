using System.Threading.Tasks;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField]
    float rotationRate = 1.0f;

    [HideInInspector]
    public int score = 0;

    Transform panelTransform;
    delegate void State();
    State currentState;
    private int rotatedAmount;

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
}
