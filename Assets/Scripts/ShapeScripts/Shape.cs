using UnityEngine;

public class Shape : MonoBehaviour
{
    // Private:
    private Transform shapeTransform;
    private Material cubeMat;
    private static int panelSide = 0;
    private float time = 0;
    private float dropRate = 0.3f;
    private float range = 1.0f;

    // Public:
    public delegate void State();
    public State currentState;

    // Inspector Variables:
    public LayerMask layerMask;
    [SerializeField] ShapeVisual shapeVisual;


    // Get Cube Position
    void Start()
    {
        shapeTransform = GetComponent<Transform>();
        cubeMat = GetComponent<Renderer>().material;
        currentState = DropShape;
    }

    // Move Cube
    void Update()
    {
        currentState.Invoke();
        HandleOpacity();
    }

    // Command Pattern for Handing Shape Movement - Emulating DPad GB-Like Movement
    void MoveShape()
    {
        Vector3 direction = GetMoveDirection();

        // if direction isnt default, move and clamp
        if (direction != Vector3.zero)
        {
            Move(direction);
        }
    }

    // Get Move Direction based on Panel Side
    Vector3 GetMoveDirection()
    {
        Vector3[][] directions = new Vector3[4][];
        directions[0] = GetFrontSideDirection();
        directions[1] = GetRightSideDirection();
        directions[2] = GetBackSideDirection();
        directions[3] = GetLeftSideDirection();

        // Check for Input
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            // Check if shape can move in that direction, return direction if no collision
            if (!CheckCollision(directions[0][0])) return directions[panelSide][0];
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (!CheckCollision(directions[0][1])) return directions[panelSide][1];
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (!CheckCollision(directions[0][2])) return directions[panelSide][2];
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (!CheckCollision(directions[0][3])) return directions[panelSide][3];
        }

        return Vector3.zero;
    }

    // All Possible Directions based on Panel Sides
    Vector3[] GetFrontSideDirection()
    {
        return new Vector3[] { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
    }

    Vector3[] GetRightSideDirection()
    {
        return new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
    }

    Vector3[] GetBackSideDirection()
    {
        return new Vector3[] { Vector3.left, Vector3.right, Vector3.back, Vector3.forward };
    }

    Vector3[] GetLeftSideDirection()
    {
        return new Vector3[] { Vector3.back, Vector3.forward, Vector3.right, Vector3.left };
    }

    // Collison detection for shape to ensure it can or cannot move on the x/z axis
    public bool CheckCollision(Vector3 direction)
    {
        // check if the cube is colliding with anything
        RaycastHit hit;

        // Detect collision with the object condition of parent
        if (Physics.Raycast(shapeTransform.position, direction, out hit, range, layerMask))
        {
            // Check if the object hit by the raycast is not part of the same shape
            if (hit.transform != transform && hit.transform.parent != transform)
            {
                // If the cube is colliding with something else, return true - shape is not allowed to move
                return true;
            }
        }

        // Next, detect collisions for the child cubes of the shape and checking if they are colliding with anything
        foreach (Transform child in transform)
        {
            if (Physics.Raycast(child.position, direction, out hit, range, layerMask))
            {
                // Check if the object hit by the raycast is not part of the same shape
                if (hit.transform != transform && hit.transform.parent != transform)
                {
                    // If the cube is colliding with something else, return true - shape is not allowed to move
                    return true;
                }
                //HELLO, I HOPE YOU HAD A GREAT DAY! LOVE YA, NIGHT NIGHT
            }
        }

        // if no collision detected, return false - shape is allowed to move
        return false;
    }

    // Overloaded CheckCollision method to allow for custom range of detection and with a basic condition
    public bool CheckCollision(Vector3 direction, Transform objectTransform, float range = 1.0f)
    {
        // check if the cube is colliding with anything
        RaycastHit hit;

        // Detect collision with the object condition
        if (Physics.Raycast(objectTransform.position, direction, out hit, range, layerMask))
        {
            // Check if the object hit by the raycast is not part of the same shape
            if (hit.transform != transform && hit.transform.parent != transform)
            {
                // If the cube is colliding with something else, return true - shape is not allowed to move
                return true;
            }
        }

        // if no collision detected, return false - shape is allowed to move
        return false;
    }

    // Move and ensure that Shape is within bounds
    void Move(Vector3 direction)
    {
        // move cube
        shapeTransform.localPosition += direction;
    }

    // Handle Shape Sides
    public void HandleShapeSides(int direction)
    {
        panelSide += 1 * direction; // direction is either 1 or -1, so -1 if rotating left, 1 if right rotating right

        // if panel side is greater than 3, reset to 0
        if (panelSide > 3)
        {
            panelSide = 0;
        }

        // if panel side is less than 0, reset to 3
        if (panelSide < 0)
        {
            panelSide = 3;
        }
    }

    // Drop Shape State
    public void DropShape()
    {
        // you are able to move the cube while it drops
        MoveShape();
        MoveDownEverySecond();
    }

    // Drop Shape State without Input State (for when rows are cleared and gravity needs to do it's job)
    public void DropShapeNoInput() => MoveDownEverySecond();

    // Move Shape Down Every Second
    void MoveDownEverySecond()
    {
        // move cube down every second
        if (time * Time.deltaTime >= 10 * dropRate)
        {
            shapeTransform.localPosition += Vector3.down;
            time = 0;
        }

        // increment time if it hasnt reached 1 second
        time++;

    }

    // stop cube from moving
    public void StopShape()
    {
        // stop spawning visual cube for it
        if (shapeVisual != null)
        {
            shapeVisual.enabled = false;
        }
    }

    public void ResetPanelSide()
    {
        // reset panel side to 0 (default)
        panelSide = 0;
    }

    void HandleOpacity()
    {
        // get distance of cube from camera
        float distance = Vector3.Distance(Camera.main.transform.position, shapeTransform.position);

        // get opacity based on distance
        float opacity = distance * 0.08f;

        // set opacity of cube material
        cubeMat.color = new Color(cubeMat.color.r, cubeMat.color.g, cubeMat.color.b, opacity);

        // update all cube sides to match the main cube 
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().material.color = new Color(child.GetComponent<Renderer>().material.color.r, child.GetComponent<Renderer>().material.color.g, child.GetComponent<Renderer>().material.color.b, cubeMat.color.a);
        }
    }
}
