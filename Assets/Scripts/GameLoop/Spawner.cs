using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] shapes;
    int shapeIndex;
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
            // if a shape is still dropping and isnt in the process of being destroyed via roll clear, return
            if (shape.GetComponent<CubeTest>().currentState == shape.GetComponent<CubeTest>().DropCube && !panelManager.handlingClearedRow)
                return;
        }

        // spawn shape at spawner position
        shapeIndex = Random.Range(0, shapes.Length);
        GameObject currentShape = Instantiate(shapes[shapeIndex], transform.position, Quaternion.identity);
        currentShape.transform.SetParent(transform);

        // ensure shape is facing forward initially
        currentShape.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
