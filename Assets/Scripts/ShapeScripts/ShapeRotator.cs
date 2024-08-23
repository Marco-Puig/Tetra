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
        // if this isnt the active shape, return
        if (GetComponent<Shape>().currentState == GetComponent<Shape>().StopShape)
        {
            return;
        }

        // Rotate shape + 90 degrees on Y axis by pressing space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // EDGE CASE (literally lol):
            // if shape is on edge of bounds, skip. This is to prevent rotating the shape when it is on the edge of the bounds
            if (Clipping())
            {
                rotationSide++;
            }

            // TODO : need to account for all 4 sides

            // NORMAL CASE:
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

    // check the possibility that if shape rotates, it doesn't collide with any other shapes
    bool Clipping()
    {
        RaycastHit hit;

        if (Physics.Raycast(GetComponent<ShapeVisual>().rightPiece.transform, Vector3.forward, out hit, 1f, LayerMask.GetMask("Bounds")))
        {
            // Vector3.right || Vector3.left - not Vector3.forward
            // do vecot 3 right or left from the right piece so it doesnt clip into shpes as well
            return true;
        }

        return false;
    }
}
