using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] shapes;
    int shapeIndex, lastRoll;
    [SerializeField] PanelManager panelManager;

    private void Update()
    {
        SpawnShape();
    }

    void SpawnShape()
    {
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
        }

        // spawn shape at spawner position
        lastRoll = shapeIndex;
        shapeIndex = Random.Range(0, shapes.Length);

        // if random roll is same as last time, do a coin toss to determine if we should roll again
        if (shapeIndex == lastRoll)
        {
            // 50% chance to roll again
            if (Random.Range(0, 1) == 0)
            {
                shapeIndex = Random.Range(0, shapes.Length);
            }
        }

        // spawn shape
        GameObject currentShape = Instantiate(shapes[shapeIndex], transform.position, Quaternion.identity);
        currentShape.transform.SetParent(transform);

        // ensure shape is facing forward initially
        currentShape.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
