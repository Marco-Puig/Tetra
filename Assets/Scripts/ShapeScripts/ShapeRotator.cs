using UnityEngine;

public class ShapeRotator : MonoBehaviour
{
    private int rotationSide = 0;

    void Update()
    {
        RotateShape();
        CheckClipping();
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

    // check the possibility that if shape rotates, it doesn't collide with any other shapes
    void CheckClipping()
    {
        // Get all colliders attached to the shape
        Collider[] colliders = GetComponentsInChildren<Collider>();

        // Check if any of the colliders are overlapping with other colliders
        foreach (Collider collider in colliders)
        {
            // Check for collisions with other colliders and if the tag is not "Shape"
            if (collider.bounds.Intersects(GetComponent<Collider>().bounds) && collider.tag != "Shape")
            {
                // Shape is colliding with another shape, so undo the rotation
                rotationSide--;

                // If rotationSide is less than 0, set it to 3 since we only have 4 sides
                rotationSide = rotationSide < 0 ? 3 : rotationSide;

                // Reset the rotation of the shape
                transform.rotation = Quaternion.Euler(transform.rotation.x, 90f * rotationSide, transform.rotation.z);

                // Exit the loop since we found a collision
                break;
            }
        }
    }
}
