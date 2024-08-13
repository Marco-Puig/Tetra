using UnityEngine;

// ONLY ATTACH THIS SCRIPT TO BOTTOM PIECES OF THE SHAPE

public class ShapeCollision : MonoBehaviour
{
    [SerializeField] Shape shape;

    // if collides with Visual object, destroy Visual object
    private void OnTriggerEnter(Collider other)
    {
        // destroy visual if it is hit
        if (other.gameObject.CompareTag("Visual"))
        {
            Destroy(other.gameObject);
        }

        // if cube is not stopped
        if (shape.currentState != shape.StopShape && !other.gameObject.CompareTag("Layer") && !other.gameObject.CompareTag("Visual"))
        {
            // destroy visual
            GameObject visualShape = GameObject.FindGameObjectWithTag("Visual");
            if (visualShape != null) Destroy(visualShape);

            // stop cube from moving:

            // move up parent
            if (transform.parent.GetComponent<Shape>() != null)
                transform.parent.position += Vector3.up;
            else
                transform.position += Vector3.up;

            // set cube to stop cube state
            shape.currentState = shape.StopShape;
        }
    }
}
