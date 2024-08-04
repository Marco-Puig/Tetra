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
            // if nothing, then return default (null)
            default:
                return Vector3.zero;
        }
    }

    Vector3 GetFrontSideDirection()
    {
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
        // if cube is not stopped
        if (currentState != StopCube && !other.gameObject.CompareTag("Layer"))
        {
            currentState = StopCube;

            if (other.gameObject.CompareTag("Visual"))
            {
                // destroy visual object
                Destroy(other.gameObject);
            }
            else
            {
                transform.position += Vector3.up;
            }
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
            //Debug.Log("Moving Cube Down");
            cubeTransform.localPosition += Vector3.down;
            //cubeVisual.transform.localPosition += Vector3.up;
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
