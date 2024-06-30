using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerPiece : MonoBehaviour
{
    [HideInInspector]
    public bool isInPiece = false;
    public bool clearingRow = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
            isInPiece = true;

            if (clearingRow)
            {
                Destroy(other.gameObject);
            }
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
            isInPiece = false;

            if (clearingRow)
            {
                Destroy(other.gameObject);
            }
        }
    }

}
