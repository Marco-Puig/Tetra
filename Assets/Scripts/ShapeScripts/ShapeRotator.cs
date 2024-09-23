using UnityEngine;

public class ShapeRotator : MonoBehaviour
{
    private int rotationSide = 0;
    void Update()
    {
        RotateShape();
    }

    void RotateShape()
    {
        // if panel manager is rotating the panel, return
        if (PanelManager.instance.currentState != PanelManager.instance.RotateOnInput)
        {
            return;
        }

        // if this isnt the active shape, return
        if (GetComponent<Shape>().currentState == GetComponent<Shape>().StopShape)
        {
            return;
        }

        // Rotate shape + 90 degrees on Y axis by pressing space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // increment rotationSide
            rotationSide++;

            // if rotationSide is greater than 3, reset it to 0 since we only have 4 sides
            if (rotationSide > 3)
            {
                rotationSide = 0;
            }

            // Check rotationSide and update the rotation of the shape accordingly
            transform.rotation = Quaternion.Euler(transform.rotation.x, 90f * rotationSide, transform.rotation.z);
        }
    }

    bool once = false;
    // check the possibility that if shape rotates, it doesn't collide with any other shapes
    public void IncrementRotationSide()
    {
        // make sure the collision check runs this code once, not multiple times
        if (once)
        {
            once = false;
            return;
        }

        rotationSide++;
        transform.rotation = Quaternion.Euler(transform.rotation.x, 90f * rotationSide, transform.rotation.z);
        once = true;
    }

}
