using UnityEngine;

public class LayerPiece : MonoBehaviour
{
    public bool isInPiece = false;
    [HideInInspector] public bool clearingRow = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
            isInPiece = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
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
}
