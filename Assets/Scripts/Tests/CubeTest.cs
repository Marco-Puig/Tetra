using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    public GameObject cubeVisual;
    Transform cubeTransform;
    Material cubeMat;
    public delegate void State();
    public State currentState;
    static int cubeSide = 0;
    float time = 0;
    public float dropRate = 0.5f;

    // Get Cube Position
    void Start()
    {
        cubeTransform = GetComponent<Transform>();
        cubeMat = GetComponent<Renderer>().material;
        currentState = DropCube;
    }

    // Move Cube
    void Update()
    {
        currentState.Invoke();
    }

    // Command Pattern for Handing Cube Movement - Emulating DPad GB-Like Movement
    void MoveCube()
    {
        Vector3 direction = GetMoveDirection();

        // if direction isnt default, move and clamp
        if (direction != Vector3.zero)
        {
            MoveAndClamp(direction);
        }
    }

    Vector3 GetMoveDirection()
    {
        switch (cubeSide)
        {
            // facing front side
            case 0:
                return GetFrontSideDirection();
            // facing right side
            case 1:
                return GetRightSideDirection();
            // facing back side
            case 2:
                return GetBackSideDirection();
            // facing left side
            case 3:
                return GetLeftSideDirection();
            // if nothing, then return default (null)
            default:
                return Vector3.zero;
        }
    }

    Vector3 GetFrontSideDirection()
    {
        // Set opacity depending on which z layer the cubes
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

        if (Input.GetKeyUp(KeyCode.RightArrow))
            return Vector3.right;
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            return Vector3.left;
        if (Input.GetKeyUp(KeyCode.UpArrow))
            return Vector3.forward;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            return Vector3.back;
        return Vector3.zero;
    }

    Vector3 GetRightSideDirection()
    {
        // Set opacity depending on which x layer the cubes
        switch (cubeTransform.localPosition.x)
        {
            case -1.0f:
                cubeMat.color = new Color(cubeMat.color.r, cubeMat.color.g, cubeMat.color.b, 1.0f);
                break;
            case 0.0f:
                cubeMat.color = new Color(cubeMat.color.r, cubeMat.color.g, cubeMat.color.b, 0.7f);
                break;
            case 1.0f:
                cubeMat.color = new Color(cubeMat.color.r, cubeMat.color.g, cubeMat.color.b, 0.4f);
                break;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
            return Vector3.forward;
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            return Vector3.back;
        if (Input.GetKeyUp(KeyCode.UpArrow))
            return Vector3.left;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            return Vector3.right;
        return Vector3.zero;
    }

    Vector3 GetBackSideDirection()
    {
        // Set opacity depending on which z layer the cubes
        switch (cubeTransform.localPosition.z)
        {
            case -1.0f:
                cubeMat.color = new Color(cubeMat.color.r, cubeMat.color.g, cubeMat.color.b, 1.0f);
                break;
            case 0.0f:
                cubeMat.color = new Color(cubeMat.color.r, cubeMat.color.g, cubeMat.color.b, 0.7f);
                break;
            case 1.0f:
                cubeMat.color = new Color(cubeMat.color.r, cubeMat.color.g, cubeMat.color.b, 0.4f);
                break;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
            return Vector3.left;
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            return Vector3.right;
        if (Input.GetKeyUp(KeyCode.UpArrow))
            return Vector3.back;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            return Vector3.forward;
        return Vector3.zero;
    }

    Vector3 GetLeftSideDirection()
    {
        // Set opacity depending on which x layer the cubes
        switch (cubeTransform.localPosition.x)
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

        if (Input.GetKeyUp(KeyCode.RightArrow))
            return Vector3.back;
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            return Vector3.forward;
        if (Input.GetKeyUp(KeyCode.UpArrow))
            return Vector3.right;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            return Vector3.left;
        return Vector3.zero;
    }

    void MoveAndClamp(Vector3 direction)
    {
        // move cube
        cubeTransform.localPosition += direction;
        // ensure cube is within bounds
        cubeTransform.localPosition = new Vector3(
            Mathf.Clamp(cubeTransform.localPosition.x, -1.0f, 1.0f),
            cubeTransform.localPosition.y,
            Mathf.Clamp(cubeTransform.localPosition.z, -1.0f, 1.0f)
        );
    }

    // Handle Cube Sides
    void HandleCubeSides()
    {
        // key code a - left side of the cube and key code d - right side of the cube
        if (Input.GetKeyDown(KeyCode.A))
        {
            cubeSide--;
            if (cubeSide < 0)
            {
                cubeSide = 3;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            cubeSide++;
            if (cubeSide > 3)
            {
                cubeSide = 0;
            }
        }
    }

    // if collides with Visual object, destroy Visual object
    private void OnTriggerEnter(Collider other)
    {
        currentState = StopCube;
        Destroy(cubeVisual);
    }

    // Drop Cube
    public void DropCube()
    {
        // you are able to move the cube while it drops
        MoveCube();

        // handle cube sides
        HandleCubeSides();

        // move cube down every second
        if (time * Time.deltaTime >= 10 * dropRate)
        {
            //Debug.Log("Moving Cube Down");
            cubeTransform.localPosition += Vector3.down;
            cubeVisual.transform.localPosition += Vector3.up;
            time = 0;
        }
        else
        {
            time++;
        }
    }

    void StopCube()
    {
        // stop cube from moving
    }
}
