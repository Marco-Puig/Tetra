using UnityEngine;

public class LayerPiece : MonoBehaviour
{
    public bool isInPiece = false;
    [HideInInspector] public bool clearingRow = false;
    GameObject shapeInPiece;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Shape" && (other.gameObject.GetComponent<Shape>() != null || other.gameObject.transform.parent.gameObject.GetComponent<Shape>() != null))
        {
            if (other.gameObject.GetComponent<Shape>() != null) // so unity doesnt throw null reference exception
            {
                // dont do anything if shape is dropping cube
                if (other.gameObject.GetComponent<Shape>().currentState == other.gameObject.GetComponent<Shape>().DropShape)
                {
                    return;
                }
            }
            else
            {
                // dont do anything if shape is dropping cube - parent shape
                if (other.gameObject.transform.parent.gameObject.GetComponent<Shape>().currentState == other.gameObject.transform.parent.gameObject.GetComponent<Shape>().DropShape)
                {
                    return;
                }
            }

            isInPiece = true;
            shapeInPiece = other.gameObject; // reference to shape in piece for move down case if row under is cleared
            if (clearingRow)
            {
                // get parent of shape if it doesnt have Shape script
                if (shapeInPiece.GetComponent<Shape>() == null)
                {
                    // set shape to stop cube state to parent shape
                    other.gameObject.transform.parent.gameObject.GetComponent<Shape>().currentState = other.gameObject.transform.parent.gameObject.GetComponent<Shape>().StopShape;
                }
                else
                {
                    // set shape to stop cube state 
                    other.gameObject.GetComponent<Shape>().currentState = other.gameObject.GetComponent<Shape>().StopShape;
                }

                // destroy shape/cube in piece
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

        // get parent of shape if it doesnt have Shape script
        if (shapeInPiece.GetComponent<Shape>() == null)
        {
            shapeInPiece = shapeInPiece.transform.parent.gameObject;
        }

        // if cube is already dropping, dont do anything
        if (shapeInPiece.GetComponent<Shape>().currentState != shapeInPiece.GetComponent<Shape>().DropShapeNoInput)
        {
            // drop cube state so it moves down
            shapeInPiece.GetComponent<Shape>().currentState = shapeInPiece.GetComponent<Shape>().DropShapeNoInput;
        }
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
