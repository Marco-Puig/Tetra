using System;
using System.Threading.Tasks;
using UnityEngine;

public class LayerPiece : MonoBehaviour
{
    public bool isInPiece = false;
    [HideInInspector] public bool clearingRow = false;
    private GameObject shapeInPiece;
    Action moveDownShape;

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
                // 'destroy' shape/cube in piece
                ClearPiece(other.gameObject);

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

    void Update()
    {
        if (moveDownShape != null)
        {
            moveDownShape.Invoke();
        }
    }

    // private method to clear shape in piece
    private void ClearPiece(GameObject piece)
    {
        piece.GetComponent<MeshRenderer>().enabled = false;
        piece.GetComponent<BoxCollider>().enabled = false;

        if (piece.GetComponent<Shape>() != null) piece.GetComponent<Shape>().currentState = null;
        else piece.transform.parent.GetComponent<Shape>().currentState = null;

    }

    // public method to move shape in piece down
    public void MoveDownShape()
    {
        if (shapeInPiece == null)
        {
            Debug.LogError("Shape in piece is null");
            return;
        }
        moveDownShape = ShapeDownState;
    }

    bool once = false;
    async void ShapeDownState()
    {
        await Task.Delay(500);
        if (once) { once = false; return; }
        if (shapeInPiece == null) return; // if shape is destroyed, return
        // move shape in piece down and make sure that it has move down once by check the previous pos of shape
        shapeInPiece.transform.localPosition += Vector3.down;
        moveDownShape = null;
        once = true;
    }
}
