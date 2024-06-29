using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerPiece : MonoBehaviour
{
    public bool isInPiece = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
            isInPiece = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
            isInPiece = false;
        }
    }

}
