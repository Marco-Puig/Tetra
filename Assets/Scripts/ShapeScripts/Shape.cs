using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour
{
    // Private: - some of these will be moved to public once code is more finialized
    private Transform shapeTransform;
    private Material cubeMat;
    private float time = 0;
    private float range = 1.0f; // for collision detection
    private static float dropRate = 2.0f;
    private float glowDuration = 1.0f; // Total time to glow and return to original

    // Public:
    public delegate void State();
    public State currentState;
    public static int panelSide = 0; // 0 = front, 1 = right, 2 = back, 3 = left
    [HideInInspector] public bool[] directionsBool = new bool[4];

    // Inspector Variables:
    public LayerMask layerMask;
    [SerializeField] ShapeVisual shapeVisual;
    [SerializeField] AudioClip soundEffect;

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
        if (currentState != null) currentState.Invoke();
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
        // if paused, return default direction
        if (PauseMenu.isPaused) return Vector3.zero;

        // Get all possible directions based on panel side
        Vector3[][] directions = new Vector3[4][];
        directions[0] = GetFrontSideDirection();
        directions[1] = GetRightSideDirection();
        directions[2] = GetBackSideDirection();
        directions[3] = GetLeftSideDirection();

        // Check for Input
        if (directionsBool[0])
        {
            // Check if shape can move in that direction, return direction if no collision
            if (!CheckCollision(directions[0][0])) 
            {
                directionsBool[0] = false;
                return directions[panelSide][0];
            }
        }
        if (directionsBool[1])
        {
            if (!CheckCollision(directions[0][1])) 
            { 
                directionsBool[1] = false;
                return directions[panelSide][1];
            }
        }
        if (directionsBool[2])
        {
            if (!CheckCollision(directions[0][2])) 
            { 
                directionsBool[2] = false;
                return directions[panelSide][2];
            }
        }
        if (directionsBool[3])
        {
            if (!CheckCollision(directions[0][3])) 
            { 
                directionsBool[3] = false;
                return directions[panelSide][3];
            }
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

    // Overloaded CheckCollision method to allow for custom range of detection and with a basic condition
    public bool CheckCollision(Vector3 direction, Transform objectTransform, LayerMask layers, float range = 1.0f)
    {
        // check if the cube is colliding with anything
        RaycastHit hit;

        // Detect collision with the object condition
        if (Physics.Raycast(objectTransform.position, direction, out hit, range, layers))
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
        CheckSlam();
    }

    // Move Shape Down Every Second
    public void MoveDownEverySecond()
    {
        // calculate drop rate based on amount of shapes in the scene
        dropRate = CalculateDropRate();

        // move cube down every second
        if ((time * Time.deltaTime) >= dropRate)
        {
            shapeTransform.localPosition += Vector3.down;
            time = 0;
        }

        // increment time if it hasnt reached 1 second
        time++;
    }

    float CalculateDropRate()
    {
        // calculate drop rate based on amount of shapes in the scene
        GameObject[] shapesInScene = GameObject.FindGameObjectsWithTag("Shape");
        // calculate drop rate based on amount of shapes in the scene
        if (shapesInScene.Length >= 27) return dropRate - (27 * 0.00005f);
        return dropRate - (shapesInScene.Length * 0.00005f);
    }

    // Shape Move Only when ready to stop
    public void MoveOnly()
    {
        MoveShape();
    }

    bool once = false;
    // stop cube from moving
    public void StopShape()
    {
        // Stop spawning visual cube for it
        if (shapeVisual != null)
        {
            shapeVisual.enabled = false;
        }

        // I only want this to run once
        if (once) return;

        // Play thump sound effect
        AudioManager audioManager = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioManager>();
        audioManager.PlaySound(soundEffect);

        // Do camera shake effect to show that the cube has stopped
        Camera camera = Camera.main;
        StartCoroutine(CameraSystem.CameraShake(camera, 0.08f, 1f));
        StartCoroutine(GlowShape());

        // for the next time this runs, it will not run since it is in a statemode, so it will only run once even though it is in Update()
        once = true;
    }

    IEnumerator GlowShape()
    {
        Color originalColor = cubeMat.GetColor("_EmissionColor");
        float elapsedTime = 0f;

        // Glow effect (increase emission)
        while (elapsedTime < glowDuration / 2f)
        {
            float t = elapsedTime / (glowDuration / 2f);
            Color glowColor = Color.Lerp(originalColor, Color.white, t);

            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().material.SetColor("_EmissionColor", glowColor * Mathf.LinearToGammaSpace(1.0f));
            }

            GetComponent<Renderer>().material.SetColor("_EmissionColor", glowColor * Mathf.LinearToGammaSpace(1.0f));

            elapsedTime += Time.unscaledDeltaTime;
            yield return null; // Wait for the next frame
        }

        yield return new WaitForSeconds(0.25f); // Keep glowing for a moment

        // Un-glow effect (fade emission back)
        elapsedTime = 0f;
        while (elapsedTime < glowDuration / 2f)
        {
            float t = elapsedTime / (glowDuration / 2f);
            Color unGlowColor = Color.Lerp(Color.white, originalColor, t);

            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().material.SetColor("_EmissionColor", unGlowColor * Mathf.LinearToGammaSpace(1.0f));
            }

            GetComponent<Renderer>().material.SetColor("_EmissionColor", unGlowColor * Mathf.LinearToGammaSpace(1.0f));

            elapsedTime += Time.unscaledDeltaTime;
            yield return null; // Wait for the next frame
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

    // Temporary Reset Drop Rate and wait for 500ms and put it back
    float tempDropRate;
    public void ClearDropRate()
    {
        tempDropRate = dropRate;
        dropRate = 2.0f;
    }
    public void ResetDropRate()
    {
        dropRate = tempDropRate;
    }

    // Check if Shape can Slam
    void CheckSlam()
    {
        // if S key is pressed, slam the piece down
        if (Input.GetKey(KeyCode.S) && !PauseMenu.isPaused)
        {
            if (!CheckCollision(Vector3.down))
            {
                Time.timeScale = 15; // 'slam' aka speed up the piece (game)
                PanelManager.instance.UpdateScore(1); // update score
            }
        }
    }
}
