using System.Threading.Tasks;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    [SerializeField] GameObject[] piecesInLayer;
    [SerializeField] PanelManager panelManager;
    [SerializeField] int layerIndex = 0;

    void Update()
    {
        CheckRow();
    }

    void CheckRow()
    {
        foreach (GameObject piece in piecesInLayer)
        {
            if (!piece.GetComponent<LayerPiece>().isInPiece || piece.GetComponent<LayerPiece>().clearingRow)
            {
                // if a piece is not in the layer, dont continue on to clear the row
                return;
            }
        }

        // Clear row
        CleanRow();

        // send info to panel manager to update score
        panelManager.UpdateScore();
    }

    void CleanRow()
    {
        foreach (GameObject piece in piecesInLayer)
        {
            piece.GetComponent<LayerPiece>().clearingRow = true;
        }

        panelManager.HandleClearedRow(layerIndex); // move all layers above down, but have panel manager handle it since it tracks all layers
    }

    public void MoveDownPieces() // move all pieces in the layer down
    {
        foreach (GameObject piece in piecesInLayer)
        {
            if (piece.GetComponent<LayerPiece>().isInPiece)
            {
                piece.GetComponent<LayerPiece>().MoveDownShape();
            }
        }
    }
}
