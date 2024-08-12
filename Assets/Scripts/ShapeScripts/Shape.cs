using UnityEngine;
using UnityEngine.AI;

public class Shape : MonoBehaviour
{
    Transform cubeTransform;
    Material cubeMat;
    public delegate void State();
    public State currentState;
    static int panelSide = 0;
    float time = 0;
    float dropRate = 0.3f;

    public LayerMask layerMask;
    public bool usesVisual = true;
    [SerializeField] GameObject leftShapePart;
    [SerializeField] GameObject rightShapePart;
    [SerializeField] GameObject topShapePart;
    [SerializeField] GameObject bottomShapePart;
    [SerializeField] CubeVisual cubeVisual;

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
        //HandleOpacity();
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
        switch (panelSide)
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
            // if nothing, then dont move (this case wont be triggered if cubside is working properly)
            default:
                return Vector3.zero;
        }
    }

    Vector3 GetFrontSideDirection()
    {
        // ray cast to see if there is another cube next to it
        RaycastHit hit;

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.right, out hit, 1 + (leftShapePart.transform.position.x - rightShapePart.transform.position.x), layerMask))
                return Vector3.zero;
            if (Physics.Raycast(rightShapePart.transform.position, Vector3.right, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.right;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.left, out hit, 1.0f, layerMask))
                return Vector3.zero;
            if (Physics.Raycast(leftShapePart.transform.position, Vector3.left, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.left;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.forward, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.forward;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.back, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.back;
        }
        return Vector3.zero;
    }

    Vector3 GetRightSideDirection()
    {
        // ray cast to see if there is another cube next to it
        RaycastHit hit;

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.right, out hit, 1 + (leftShapePart.transform.position.x - rightShapePart.transform.position.x), layerMask))
                return Vector3.zero;
            if (Physics.Raycast(rightShapePart.transform.position, Vector3.right, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.forward;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.left, out hit, 1.0f, layerMask))
                return Vector3.zero;
            if (Physics.Raycast(leftShapePart.transform.position, Vector3.left, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.back;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.forward, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.left;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.back, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.right;
        }
        return Vector3.zero;

    }

    Vector3 GetBackSideDirection()
    {
        // ray cast to see if there is another cube next to it
        RaycastHit hit;

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.right, out hit, 1 + (leftShapePart.transform.position.x - rightShapePart.transform.position.x), layerMask))
                return Vector3.zero;
            if (Physics.Raycast(rightShapePart.transform.position, Vector3.right, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.left;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.left, out hit, 1.0f, layerMask))
                return Vector3.zero;
            if (Physics.Raycast(leftShapePart.transform.position, Vector3.left, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.right;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.forward, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.back;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.back, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.forward;
        }
        return Vector3.zero;
    }

    Vector3 GetLeftSideDirection()
    {
        // ray cast to see if there is another cube next to it
        RaycastHit hit;

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.right, out hit, 1 + (leftShapePart.transform.position.x - rightShapePart.transform.position.x), layerMask))
                return Vector3.zero;
            if (Physics.Raycast(rightShapePart.transform.position, Vector3.right, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.back;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.left, out hit, 1.0f, layerMask))
                return Vector3.zero;
            if (Physics.Raycast(leftShapePart.transform.position, Vector3.left, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.forward;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.forward, out hit, 1.0f, layerMask))
                return Vector3.zero;
            if (Physics.Raycast(leftShapePart.transform.position, Vector3.forward, out hit, 1.0f, layerMask))
                return Vector3.zero;
            if (Physics.Raycast(rightShapePart.transform.position, Vector3.forward, out hit, 1.0f, layerMask))
                return Vector3.zero;
            if (Physics.Raycast(topShapePart.transform.position, Vector3.forward, out hit, 1.0f, layerMask))
                return Vector3.zero;
            if (Physics.Raycast(bottomShapePart.transform.position, Vector3.forward, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.right;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.back, out hit, 1.0f, layerMask))
                return Vector3.zero;
            if (Physics.Raycast(leftShapePart.transform.position, Vector3.back, out hit, 1.0f, layerMask))
                return Vector3.zero;
            if (Physics.Raycast(rightShapePart.transform.position, Vector3.back, out hit, 1.0f, layerMask))
                return Vector3.zero;
            if (Physics.Raycast(topShapePart.transform.position, Vector3.back, out hit, 1.0f, layerMask))
                return Vector3.zero;
            if (Physics.Raycast(bottomShapePart.transform.position, Vector3.back, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.left;
        }
        return Vector3.zero;
    }

    void MoveAndClamp(Vector3 direction)
    {
        // move cube
        cubeTransform.localPosition += direction;
        // ensure cube is within bounds
        cubeTransform.localPosition = new Vector3(
            Mathf.Clamp(cubeTransform.localPosition.x, -3.0f, 1.0f), // TEMPORARY FIX for clamping x
            cubeTransform.localPosition.y,
            Mathf.Clamp(cubeTransform.localPosition.z, -3.0f, 2.0f)
        );
    }

    // Handle Cube Sides
    public void HandleCubeSides(int direction)
    {
        panelSide += 1 * direction;

        if (panelSide < 0)
        {
            panelSide = 3;
        }

        if (panelSide > 3)
        {
            panelSide = 0;
        }
    }

    // Drop Cube
    public void DropCube()
    {
        // you are able to move the cube while it drops
        MoveCube();

        // move cube down every second
        if (time * Time.deltaTime >= 10 * dropRate)
        {
            cubeTransform.localPosition += Vector3.down;
            time = 0;
        }
        else
        {
            time++;
        }
    }

    public void DropCubeNoInput()
    {
        // move cube down every second
        if (time * Time.deltaTime >= 10 * dropRate)
        {
            cubeTransform.localPosition += Vector3.down;
            time = 0;
        }
        else
        {
            time++;
        }
    }

    public void StopCube()
    {
        // stop cube from moving

        // stop spawning visual cube for it
        if (usesVisual)
            cubeVisual.enabled = false;
    }

    void HandleOpacity()
    {
        // REDO HANDLING OPACITY BASED ON DISTANCE OF CUBE TOWARDS THE CAMERA

        // update all cube sides to match the main cube 
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().material.color = new Color(child.GetComponent<Renderer>().material.color.r, child.GetComponent<Renderer>().material.color.g, child.GetComponent<Renderer>().material.color.b, cubeMat.color.a);
        }
    }
}
