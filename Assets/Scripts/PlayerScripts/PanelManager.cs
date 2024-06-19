using System.Threading.Tasks;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField]
    float rotationDegrees = 90.0f;
    Transform panelTransform;
    delegate void State();
    State currentState;

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
            RotatePanel(new Vector3(0, rotationDegrees, 0));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            RotatePanel(new Vector3(0, rotationDegrees, 0));
        }
    }
    async void RotatePanel(Vector3 direction)
    {
        for (int i = 0; i < 4; i++)
        {
            RotatePanel(direction);
            await Task.Delay(1000);
        }
    }
}
