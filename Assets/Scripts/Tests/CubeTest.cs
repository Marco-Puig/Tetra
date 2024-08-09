using UnityEngine;

public class CubeTest : MonoBehaviour
{
    Transform cubeTransform;
    Material cubeMat;
    public delegate void State();
    public State currentState;
    static int cubeSide = 0;
    float time = 0;
    float dropRate = 0.3f;

    public LayerMask layerMask;

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
        HandleOpacity();
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
            if (Physics.Raycast(cubeTransform.position, Vector3.right, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.right;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.left, out hit, 1.0f, layerMask))
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
            if (Physics.Raycast(cubeTransform.position, Vector3.forward, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.forward;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.back, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.back;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.left, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.left;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.right, out hit, 1.0f, layerMask))
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
            if (Physics.Raycast(cubeTransform.position, Vector3.left, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.left;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.right, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.right;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.back, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.back;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.forward, out hit, 1.0f, layerMask))
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
            if (Physics.Raycast(cubeTransform.position, Vector3.back, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.back;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.forward, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.forward;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.right, out hit, 1.0f, layerMask))
                return Vector3.zero;
            return Vector3.right;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (Physics.Raycast(cubeTransform.position, Vector3.left, out hit, 1.0f, layerMask))
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
        // destroy visual if it is hit
        if (other.gameObject.CompareTag("Visual"))
        {
            Destroy(other.gameObject);
        }

        // if cube is not stopped
        if (currentState != StopCube && !other.gameObject.CompareTag("Layer") && !other.gameObject.CompareTag("Visual"))
        {
            // destroy visual
            GameObject visualCube = GameObject.FindGameObjectWithTag("Visual");
            if (visualCube != null) Destroy(visualCube);

            // stop cube from moving
            transform.position += Vector3.up;
            currentState = StopCube;
        }
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
        GetComponent<CubeVisual>().enabled = false;
    }

    void HandleOpacity()
    {
        switch (cubeSide)
        {
            case 0:
                HandleFrontSideOpacity();
                break;
            case 1:
                HandleRightSideOpacity();
                break;
            case 2:
                HandleBackSideOpacity();
                break;
            case 3:
                HandleLeftSideOpacity();
                break;
        }
    }

    void HandleFrontSideOpacity()
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

    void HandleRightSideOpacity()
    {
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
    }

    void HandleBackSideOpacity()
    {
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
    }

    void HandleLeftSideOpacity()
    {
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
    }

}
