using System.Threading.Tasks;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public GameObject[] piecesInLayer;
    byte count = 0;
    void LateUpdate()
    {
        CheckRow();
    }

    async void CheckRow()
    {
        // wait every few ticks to check if the layer is complete
        await Task.Delay(1000);

        foreach (GameObject piece in piecesInLayer)
        {
            if (piece.GetComponent<LayerPiece>().isInPiece)
            {
                count++;

                // if the layer is complete
                if (count == piecesInLayer.Length)
                {
                    Debug.Log("Layer Cleared");
                    // send info to panel manager to update score
                    CleanRow();
                    break;
                }
            }
        }
    }

    async void CleanRow()
    {
        foreach (GameObject piece in piecesInLayer)
        {
            piece.GetComponent<LayerPiece>().clearingRow = true;
            await Task.Delay(1000); // wait for piece to clear
            piece.GetComponent<LayerPiece>().clearingRow = false;
        }
    }
}
