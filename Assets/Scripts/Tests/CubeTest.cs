using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    Transform cubeTransform;
    Material cubeMat;
    delegate void State();
    State currentState;

    // Get Cube Position
    void Start()
    {
        cubeTransform = GetComponent<Transform>();
        cubeMat = GetComponent<Renderer>().material;
        currentState = MoveCube;
    }

    // Move Cube
    void Update()
    {
        currentState.Invoke();
    }

    // Command Pattern for Handing Cube Movement - Emulating DPad GB-Like Movement
    void MoveCube()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            cubeTransform.localPosition += Vector3.right;
            cubeTransform.localPosition = new Vector3(Mathf.Clamp(cubeTransform.localPosition.x, -1.0f, 1.0f), cubeTransform.localPosition.y, cubeTransform.localPosition.z);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            cubeTransform.localPosition += Vector3.left;
            cubeTransform.localPosition = new Vector3(Mathf.Clamp(cubeTransform.localPosition.x, -1.0f, 1.0f), cubeTransform.localPosition.y, cubeTransform.localPosition.z);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            cubeTransform.localPosition += Vector3.forward;
            cubeTransform.localPosition = new Vector3(cubeTransform.localPosition.x, cubeTransform.localPosition.y, Mathf.Clamp(cubeTransform.localPosition.z, -1.0f, 1.0f));
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            cubeTransform.localPosition += Vector3.back;
            cubeTransform.localPosition = new Vector3(cubeTransform.localPosition.x, cubeTransform.localPosition.y, Mathf.Clamp(cubeTransform.localPosition.z, -1.0f, 1.0f));
        }

        SetOpacity();
    }

    // Set opacity depending on which z layer the cubes
    void SetOpacity()
    {
        switch (cubeTransform.localPosition.z)
        {
            case -1.0f:
                cubeMat.color = new Color(cubeMat.color.r, cubeMat.color.g, cubeMat.color.b, 0.4f);
                break;
            case 0.0f:
                cubeMat.color = new Color(cubeMat.color.r, cubeMat.color.g, cubeMat.color.b, 0.7f);
                break;
            case 1.0f:
                cubeMat.color = new Color(cubeMat.color.r, cubeMat.color.g, cubeMat.color.b, 1.0f);
                break;
        }
    }
}
