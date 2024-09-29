using UnityEngine;

public class LayerManager : MonoBehaviour
{
    [SerializeField] GameObject[] piecesInLayer;
    [SerializeField] PanelManager panelManager;

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
    }
}
