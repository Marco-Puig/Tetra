using System;
using UnityEngine;

public class LayerPiece : MonoBehaviour
{
    public bool isInPiece = false;
    [SerializeField] LayerMask layerMask;
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
        // CheckBelow(shapeInPiece);
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
        // if shape is null, return
        if (piece == null) return;

        // if piece is on the floor, return
        if (piece.transform.position.y <= 1f) return;

        // if shape isnt on top of another shape, move down
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, 1f, layerMask)) // layerMask to to check for ground or shape below
        {
            // if there is no hit, move shape down
            piece.transform.parent = GameObject.Find("Spawner").transform;
            piece.transform.localPosition += Vector3.down; // move shape down
        }
    }
}
