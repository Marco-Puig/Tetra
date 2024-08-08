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
            isInPiece = true;
            shapeInPiece = other.gameObject; // reference to shape in piece for move down case if row under is cleared
            if (clearingRow)
            {
                Destroy(other.gameObject);

                // once cleared, stop clear action
                isInPiece = false;

                // once cleared, stop clear action
                clearingRow = false;
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
    public void MoveDown()
    {
        if (shapeInPiece == null)
        {
            Debug.LogError("Shape in piece is null"); //shouldnt happen if all pieces are in layer to even start clearing in the first place
            return;
        }
        shapeInPiece.transform.position += Vector3.down;
    }
}
