using UnityEngine;

public class LayerPiece : MonoBehaviour
{
    public bool isInPiece = false;
    [HideInInspector] public bool clearingRow = false;
    GameObject shapeInPiece;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
            // dont do anything if shape is dropping cube
            if (other.gameObject.GetComponent<CubeTest>().currentState == other.gameObject.GetComponent<CubeTest>().DropCube)
            {
                return;
            }

            isInPiece = true;
            shapeInPiece = other.gameObject; // reference to shape in piece for move down case if row under is cleared
            if (clearingRow)
            {
                // set shape to stop cube state and destroy it
                other.gameObject.GetComponent<CubeTest>().currentState = other.gameObject.GetComponent<CubeTest>().StopCube;
                Destroy(other.gameObject);

                // once cleared, stop clear action
                clearingRow = false;

                // once cleared, stop clear action
                isInPiece = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
            isInPiece = false;
        }
    }

    // public method to move shape in piece down        
    public void MoveDownShape()
    {
        if (shapeInPiece == null)
        {
            return;
        }

        // if cube is already dropping, dont do anything
        if (shapeInPiece.GetComponent<CubeTest>().currentState == shapeInPiece.GetComponent<CubeTest>().DropCube)
        {
            return;
        }

        // drop cube state so it moves down
        shapeInPiece.GetComponent<CubeTest>().currentState = shapeInPiece.GetComponent<CubeTest>().DropCube;
    }

    // public method to ensure shape abides by grid
    public void AdjustShape()
    {
        if (shapeInPiece == null)
        {
            return;
        }
    }
}
