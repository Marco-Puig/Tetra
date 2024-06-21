using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] shapes;
    int shapeIndex;

    private void Update()
    {
        SpawnShape();
    }

    void SpawnShape()
    {
        GameObject[] spawnedShapes = GameObject.FindGameObjectsWithTag("Shape");

        // if no shapes are dropping, spawn a new shape
        foreach (GameObject shape in spawnedShapes)
        {
            // if a shape is still dropping, return
            if (shape.GetComponent<CubeTest>().currentState == shape.GetComponent<CubeTest>().DropCube)
                return;
        }

        // spawn shape at spawner position
        shapeIndex = Random.Range(0, shapes.Length);
        Instantiate(shapes[shapeIndex], transform.position, Quaternion.identity);
    }
}
