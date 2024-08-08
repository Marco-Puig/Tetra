using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
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
        start = false;
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
            await Task.Delay(100); // wait for old visual to be destroyed before creating new one

        // use raycast to spawn visual on cube the visual is on
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 6, layerMask))
        {
            // ignore if tag is "Visual" 
            if (hit.collider.gameObject.CompareTag("Visual"))
            {
                return;
            }

            CreateVisualCube(new Vector3(transform.position.x, (float)Math.Ceiling(hit.point.y), transform.position.z));
        }

        // Debug.Log(hit.point);
        Debug.DrawLine(transform.position, hit.point, Color.red, 6);
    }

    void CreateVisualCube(Vector3 position)
    {
        // clean up previous visual cube
        if (visualCube != null)
        {
            Destroy(visualCube);
        }

        visualCube = Instantiate(visualCubePrefab, position, Quaternion.identity);
        visualCube.transform.parent = transform.parent;
    }
}
