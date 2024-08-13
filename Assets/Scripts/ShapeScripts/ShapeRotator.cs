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
            // if shape is on edge of bounds and rotationSide is 1, skip. This is to prevent rotating the shape when it is on the edge of the bounds
            if (GetComponent<Shape>().edgeOfBounds && rotationSide == 1)
            {
                // skip incrementing rotationSide that would go out of bounds
                rotationSide++;
            }

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
}
