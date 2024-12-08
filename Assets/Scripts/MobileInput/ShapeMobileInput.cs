using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShapeMobileInput : MonoBehaviour
{
    Shape activeShape;
    Action slamAction;
    
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
        activeShape.directionsBool[0] = true;
    }

    public void TriggerLeftInput()
    {
        activeShape.directionsBool[1] = true;
    }

    public void TriggerUpInput()
    {
        activeShape.directionsBool[2] = true;
    }

    public void TriggerDownInput()
    {
        activeShape.directionsBool[3] = true;
    }

    // Mobile UI button rotate input method
    public void TriggerRotateInput()
    {
        if (activeShape.GetComponent<ShapeRotator>() == null) return;
        activeShape.GetComponent<ShapeRotator>().RotateShape();
    }    

    // Mobile UI button slam input method
    public void TriggerSlamInput()
    {
        // if the slam action is already in progress, then clicking again will cancel the slam action
        if (slamAction != null)
        {
            slamAction = null;
            return;
        }
        // if the slam action is not in progress, then clicking will start the slam action
        activeShape.ForceSlam();
    }
    
    // Update Visual Position based on input
    public void UpdateVisualPosition()
    {
        if (activeShape == null) return;
        if (activeShape.currentState == activeShape.MoveOnly) return;
        activeShape.GetComponent<ShapeVisual>().CalculateVisualPosition();
    }
}
