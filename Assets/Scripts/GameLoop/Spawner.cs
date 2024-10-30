using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject[] shapes;
    int shapeIndex, nextShapeIndex;
    [SerializeField] PanelManager panelManager;
    [SerializeField] GameObject[] shapeIcons;
    public static bool stopSpawning = false; // force stop override

    private void Start()
    {
        // set next shape index to random value
        nextShapeIndex = Random.Range(0, shapes.Length); // <-- techinically this is the first shape to spawn
    }
    private void Update()
    {
        if (stopSpawning) return;
        SpawnShape();
        DisplayNextShape();
    }

    void SpawnShape()
    {
        // check if allowed to spawn shape
        // get all shapes that are currently dropping
        GameObject[] spawnedShapes = GameObject.FindGameObjectsWithTag("Shape");

        // if no shapes are dropping, spawn a new shape
        foreach (GameObject shape in spawnedShapes)
        {
            // if cube isnt the main part of shape, skip
            if (shape.GetComponent<Shape>() == null)
                continue;

            // if a shape is still dropping and isnt in the process of being destroyed via roll clear, return
            if (shape.GetComponent<Shape>().currentState == shape.GetComponent<Shape>().DropShape)
                return;

            // if a shape is still dropping and isnt in the process of being destroyed via roll clear, return
            if (shape.GetComponent<Shape>().currentState == shape.GetComponent<Shape>().MoveOnly)
                return;
        }

        // spawn shape at spawner position
        shapeIndex = nextShapeIndex; // set current shape index to next shape index from prior spawn
        nextShapeIndex = Random.Range(0, shapes.Length); // create the new next shape

        // if random roll is same as last time, do a coin toss to determine if we should roll again
        if (shapeIndex == nextShapeIndex)
        {
            // 50% chance to roll again
            if (Random.Range(0, 1) == 0)
            {
                nextShapeIndex = Random.Range(0, shapes.Length);
            }
        }

        // spawn shape
        GameObject currentShape = Instantiate(shapes[shapeIndex], transform.position, Quaternion.identity);
        currentShape.transform.SetParent(transform);

        // ensure shape is facing forward initially
        currentShape.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void DisplayNextShape()
    {
        // make sure all shape icons arent showing at once
        foreach (GameObject icon in shapeIcons)
        {
            icon.SetActive(false);
        }

        // display next shape icon
        shapeIcons[nextShapeIndex].SetActive(true);
    }

    // stop spawner
    public void StopSpawning()
    {
        stopSpawning = true;
    }

    // cleanup
    void OnDisable()
    {
        stopSpawning = false;
    }
}
