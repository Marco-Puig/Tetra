using System;
using System.Threading.Tasks;
using UnityEngine;

public class CubeVisual : MonoBehaviour
{
    [SerializeField] GameObject visualCubePrefab;
    [SerializeField] LayerMask layerMask;

    private GameObject visualCube;
    bool start = true;

    void Start()
    {
        // get the ball rolling, calculate visual position and where to spawn so there is a visual off rip
        CalculateVisualPosition();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            CalculateVisualPosition();
        }

        // ensure that the visual shares the same rotation as the player cube
        if (visualCube != null)
        {
            if (visualCube.transform.rotation != transform.rotation)
            {
                visualCube.transform.rotation = transform.rotation;
            }
        }
    }

    async void CalculateVisualPosition()
    {
        if (start)
        {
            await Task.Delay(100); // wait for old visual to be destroyed before creating new one
            start = false; // set start to false so this block of code only runs once
        }

        // use raycast to spawn visual on cube the visual is on
        RaycastHit hit;

        // calculate visual position of where to spawn visual by raycasting down
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 7, layerMask)) // ignore visual and layer
        {
            CreateVisualCube(new Vector3(transform.position.x, (float)Math.Ceiling(hit.point.y), transform.position.z));
        }

        // Debug.Log(hit.point);
        Debug.DrawLine(transform.position, hit.point, Color.blue, 7);
    }

    void CreateVisualCube(Vector3 position)
    {
        // clean up previous visual cube
        if (visualCube != null)
        {
            Destroy(visualCube);
        }

        visualCube = Instantiate(visualCubePrefab, position, Quaternion.identity);

        // ensure visual cube isnt a child of the parent cube
        GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
        visualCube.transform.parent = spawner.transform;
    }

    void OnDestroy()
    {
        if (visualCube != null)
        {
            Destroy(visualCube);
        }
    }
}
