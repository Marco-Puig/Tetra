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
        CheckBelow(shapeInPiece);
    }

    // private method to clear shape in piece
    private void ClearPiece(GameObject piece)
    {
        piece.GetComponent<MeshRenderer>().enabled = false;
        piece.GetComponent<BoxCollider>().enabled = false;

        if (piece.GetComponent<Shape>() != null) piece.GetComponent<Shape>().currentState = null;
        else piece.transform.parent.GetComponent<Shape>().currentState = null;
    }

    // if a row below is cleared, check if shape is still on top of a shape or floor
    void CheckBelow(GameObject piece)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            if (hit.collider.gameObject.tag != "Shape" || hit.collider.gameObject.tag != "Ground" || hit.collider.gameObject.tag != "Layer")
            {
                // if shape is on top of another shape, move down
                // no longer do parents and children since they served there purpose already and now we can just move the pieces down consistently
                piece.transform.parent = GameObject.Find("Spawner").transform;
                piece.transform.localPosition += Vector3.down; // move shape down
            }
        }
    }
}
