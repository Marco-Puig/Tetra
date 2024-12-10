using System;
using UnityEngine;
using UnityEngine.UI;

public class ShapeMobileInput : MonoBehaviour
{
    [SerializeField] GameObject[] controlButtons;
    Shape activeShape;
    Action slamAction;
    void Start()
    {
        SyncSettings();
    }
    void Update()
    {
        FindActiveShape();
        UpdateVisualPosition();
        if (slamAction != null) slamAction();
    }

    // Find the active shape (the shape that is currently falling aka in the drop state)
    void FindActiveShape()
    {
        GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");
        if (shapes.Length == 0) return;
        
        foreach (GameObject shape in shapes)
        {
            if (shape.GetComponent<Shape>() == null)
                continue;

            if (shape.GetComponent<Shape>().currentState != shape.GetComponent<Shape>().StopShape)
            {
                activeShape = shape.GetComponent<Shape>();
            }
        }
        if (activeShape == null) return;
        if (activeShape.currentState == activeShape.StopShape)
        {
            activeShape = null;
            slamAction = null;
        }
    }

    // Mobile UI button input methods
    public void TriggerRightInput()
    {
        if (PanelManager.instance.currentState != PanelManager.instance.RotateOnInput) return;
        activeShape.directionsBool[0] = true;
    }

    public void TriggerLeftInput()
    {
        if (PanelManager.instance.currentState != PanelManager.instance.RotateOnInput) return;
        activeShape.directionsBool[1] = true;
    }

    public void TriggerUpInput()
    {
        if (PanelManager.instance.currentState != PanelManager.instance.RotateOnInput) return;
        activeShape.directionsBool[2] = true;
    }

    public void TriggerDownInput()
    {
        if (PanelManager.instance.currentState != PanelManager.instance.RotateOnInput) return;
        activeShape.directionsBool[3] = true;
    }

    // Mobile UI button rotate input method
    public void TriggerRotateInput()
    {
        if (PanelManager.instance.currentState != PanelManager.instance.RotateOnInput) return;
        if (activeShape.GetComponent<ShapeRotator>() == null) return;
        activeShape.GetComponent<ShapeRotator>().RotateShape();
    }    

    // Mobile UI button slam input method
    public void TriggerSlamInput()
    {
        if (PanelManager.instance.currentState != PanelManager.instance.RotateOnInput) return;
        // if the slam action is already in progress, then clicking again will cancel the slam action
        if (slamAction != null)
        {
            slamAction = null;
            return;
        }
        // if the slam action is not in progress, then clicking will start the slam action
        slamAction = activeShape.ForceSlam;
    }
    
    // Update Visual Position based on input
    public void UpdateVisualPosition()
    {
        if (activeShape == null) return;
        if (activeShape.currentState == activeShape.MoveOnly) return;
        activeShape.GetComponent<ShapeVisual>().CalculateVisualPosition();
    }

    // Sync the settings of the mobile input
    public void SyncSettings()
    {
        if (PlayerPrefs.GetInt("hideControls") == 1)
        {
            foreach (GameObject button in controlButtons)
            {
                // set alpha to zero
                button.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
        }
    }

    // Disable the mobile input
    public void DisableMobileInput()
    {
        foreach (GameObject button in controlButtons)
        {
            button.SetActive(false);
        }
    }

    // Enable the mobile input
    public void EnableMobileInput()
    {
        foreach (GameObject button in controlButtons)
        {
            button.SetActive(true);
        }
    }
}
