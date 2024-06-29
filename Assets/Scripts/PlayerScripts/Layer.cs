using System.Threading.Tasks;
using UnityEngine;

public class Layer : MonoBehaviour
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
                    Debug.Log("Layer Complete");
                    CleanRow();
                    break;
                }
            }
        }
    }

    void CleanRow()
    {
        foreach (GameObject piece in piecesInLayer)
        {
            Destroy(piece); // TODO: dont destoy the piece but whats inside them
        }
    }
}
