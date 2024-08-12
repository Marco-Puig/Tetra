using UnityEngine;

public class ShapeRotator : MonoBehaviour
{
    int rotationSide = 0;

    void Update()
    {
        RotateShape();
    }

    void RotateShape()
    {
        // if this isnt the active shape, return
        if (GetComponent<Shape>().currentState == GetComponent<Shape>().StopCube)
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
            transform.rotation = new Quaternion(0, 90 * rotationSide, 0, transform.rotation.w);
        }
    }
}
