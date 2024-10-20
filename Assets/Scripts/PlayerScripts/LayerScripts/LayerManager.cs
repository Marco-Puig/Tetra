using UnityEngine;

public class LayerManager : MonoBehaviour
{
    [SerializeField] GameObject[] piecesInLayer;
    [SerializeField] PanelManager panelManager;
    [SerializeField] AudioClip layerClearSFX;

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
                // if a piece is not in the layer, dont clear the row
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
        // play sound
        AudioManager audioManager = GameObject.FindGameObjectWithTag("SFXScore").GetComponent<AudioManager>();
        audioManager.PlaySound(layerClearSFX);

        // set pieces to clear mode
        foreach (GameObject piece in piecesInLayer)
        {
            piece.GetComponent<LayerPiece>().clearingRow = true;
        }
    }
}
