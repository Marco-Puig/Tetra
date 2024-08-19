using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class shapeVisual : MonoBehaviour
{
    [SerializeField] GameObject visualShapePrefab;
    [SerializeField] LayerMask layerMask;
    [SerializeField] bool hasRightPiece = true;
    [SerializeField] bool hasLeftPiece = false;
    [SerializeField] GameObject rightPiece;
    [SerializeField] GameObject leftPiece;
    private GameObject visualshape;
    bool start = true;

    void Start()
    {
        // get the ball rolling, calculate visual position and where to spawn so there is a visual off rip
        CalculateVisualPosition();
    }

    void Update()
    {
        // calculate visual position when the player cube is moved or rotated
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) ||
        Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
        Input.GetKeyUp(KeyCode.Space))
        {
            CalculateVisualPosition();
        }

        // ensure that the visual shares the same rotation as the player cube
        if (visualshape != null)
        {
            if (visualshape.transform.rotation != transform.rotation)
            {
                visualshape.transform.rotation = transform.rotation;
            }
        }
    }

    // NEEDS REWORK TO BE MORE ROBUST - CURRENTLY A BRUTE FORCE METHOD
    async void CalculateVisualPosition()
    {
        if (start)
        {
            await Task.Delay(100); // wait for old visual to be destroyed before creating new one
            start = false; // set start to false so this block of code only runs once
        }
        else
        {
            await Task.Delay(1); // wait for old visual to be destroyed before creating new one
        }

        // use raycast to spawn visual on cube the visual is on
        RaycastHit hit1;

        // use raycast to spawn visual on cube the visual is on (right stuck out piece)
        RaycastHit hit2;

        // calculate visual position of where to spawn visual by raycasting down
        if (Physics.Raycast(transform.position, Vector3.down, out hit1, 7, layerMask)) // ignore visual and layer
        {
            // ensure that right stuck out piece of visual cube is on the same level as the main shape piece
            if (Physics.Raycast(new Vector3(rightPiece.transform.position.x, rightPiece.transform.position.y, rightPiece.transform.position.z), Vector3.down, out hit2, 7, layerMask) && hasRightPiece) // ignore visual and layer
            {
                // if the visual cube is not on the same level as the main shape piece, move it up
                if (hit1.point.y < hit2.point.y)
                {
                    CreateVisualshape(new Vector3(transform.position.x, (float)Math.Ceiling(hit2.point.y - (rightPiece.transform.position.y - transform.position.y)), transform.position.z));
                }
                else
                {
                    // spawn visual cube if we are leveled correctly :)
                    CreateVisualshape(new Vector3(transform.position.x, (float)Math.Ceiling(hit1.point.y), transform.position.z));
                }
            }
            // ensure that left stuck out piece of visual cube is on the same level as the main shape piece
            if (Physics.Raycast(new Vector3(leftPiece.transform.position.x, leftPiece.transform.position.y, leftPiece.transform.position.z), Vector3.down, out hit2, 7, layerMask) && hasLeftPiece) // ignore visual and layer
            {
                // if the visual cube is not on the same level as the main shape piece, move it up
                if (hit1.point.y < hit2.point.y)
                {
                    CreateVisualshape(new Vector3(transform.position.x, (float)Math.Ceiling(hit2.point.y), transform.position.z));
                }
                else
                {
                    // spawn visual cube if we are leveled correctly :)
                    CreateVisualshape(new Vector3(transform.position.x, (float)Math.Ceiling(hit1.point.y), transform.position.z));
                }
            }

            // if shape has no left or right piece, spawn visual cube on the same level as the main shape piece
            if (!hasRightPiece && !hasLeftPiece)
            {
                CreateVisualshape(new Vector3(transform.position.x, (float)Math.Ceiling(hit1.point.y), transform.position.z));
            }

            // draw debug lines in scene view
            Debug.DrawLine(transform.position, hit1.point, Color.red, 7);
            Debug.DrawLine(transform.position, hit2.point, Color.green, 7);
        }
    }

    void CreateVisualshape(Vector3 position)
    {
        // clean up previous visual cube
        if (visualshape != null)
        {
            Destroy(visualshape);
        }

        // spawn visual cube
        visualshape = Instantiate(visualShapePrefab, position, Quaternion.identity);

        // ensure visual cube isnt a child of the parent cube
        GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
        visualshape.transform.parent = spawner.transform;
    }
}
