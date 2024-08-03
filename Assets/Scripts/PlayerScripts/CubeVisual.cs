using UnityEngine;

public class CubeVisual : MonoBehaviour
{
    [SerializeField] GameObject visualCubePrefab;

    void Start()
    {
        CreateVisualCube(Vector3.down);
    }

    void CreateVisualCube(Vector3 position)
    {
        GameObject visualCube = Instantiate(visualCubePrefab, position, Quaternion.identity);
    }
}
