using System;
using System.Threading.Tasks;
using UnityEngine;

public class CubeVisual : MonoBehaviour
{
    [SerializeField] GameObject visualCubePrefab;
    [SerializeField] LayerMask layerMask;
    [SerializeField] bool hasRightPiece = true;
    [SerializeField] bool hasLeftPiece = false;
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
        RaycastHit hit1;

        // use raycast to spawn visual on cube the visual is on (right stuck out piece)
        RaycastHit hit2;

        // calculate visual position of where to spawn visual by raycasting down
        if (Physics.Raycast(transform.position, Vector3.down, out hit1, 7, layerMask)) // ignore visual and layer
        {
            // ensure that right stuck out piece of visual cube is on the same level as the main shape piece
            if (Physics.Raycast(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Vector3.down, out hit2, 7, layerMask) && hasRightPiece) // ignore visual and layer
            {
                // if the visual cube is not on the same level as the main shape piece, move it up
                if (hit1.point.y < hit2.point.y)
                {
                    CreateVisualCube(new Vector3(transform.position.x, (float)Math.Ceiling(hit2.point.y), transform.position.z));
                }
                else
                {
                    // spawn visual cube if we are leveled correctly :)
                    CreateVisualCube(new Vector3(transform.position.x, (float)Math.Ceiling(hit1.point.y), transform.position.z));
                }
            }
            // ensure that left stuck out piece of visual cube is on the same level as the main shape piece
            if (Physics.Raycast(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Vector3.down, out hit2, 7, layerMask) && hasLeftPiece) // ignore visual and layer
            {
                // if the visual cube is not on the same level as the main shape piece, move it up
                if (hit1.point.y < hit2.point.y)
                {
                    CreateVisualCube(new Vector3(transform.position.x, (float)Math.Ceiling(hit2.point.y), transform.position.z));
                }
                else
                {
                    // spawn visual cube if we are leveled correctly :)
                    CreateVisualCube(new Vector3(transform.position.x, (float)Math.Ceiling(hit1.point.y), transform.position.z));
                }
            }

            // if shape has no left or right piece, spawn visual cube on the same level as the main shape piece
            if (!hasRightPiece && !hasLeftPiece)
            {
                CreateVisualCube(new Vector3(transform.position.x, (float)Math.Ceiling(hit1.point.y), transform.position.z));
            }

            // draw debug lines in scene view
            Debug.DrawLine(transform.position, hit1.point, Color.blue, 7);
            Debug.DrawLine(transform.position, hit2.point, Color.blue, 7);
        }
    }

    void CreateVisualCube(Vector3 position)
    {
        // clean up previous visual cube
        if (visualCube != null)
        {
            Destroy(visualCube);
        }

        // spawn visual cube
        visualCube = Instantiate(visualCubePrefab, position, Quaternion.identity);

        // ensure visual cube isnt a child of the parent cube
        GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
        visualCube.transform.parent = spawner.transform;
    }
}
