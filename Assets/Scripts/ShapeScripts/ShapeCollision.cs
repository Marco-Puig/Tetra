using UnityEngine;

// ONLY ATTACH THIS SCRIPT TO BOTTOM PIECES OF THE SHAPE

public class ShapeCollision : MonoBehaviour
{
    [SerializeField] Shape shape;
    [SerializeField] LayerMask layerMask;

    private void Start()
    {
        // get shape component from parent or current object if current object is parent
        shape = transform.GetComponent<Shape>() != null ? GetComponent<Shape>() : GetComponentInParent<Shape>();
    }


    private void Update()
    {
        // if shape is stopped, return
        if (shape.currentState == shape.StopShape) return;

        // check if shape detects shape or ground layer below it, stop shape from moving
        if (shape.CheckCollision(Vector3.down, transform, layerMask))
        {
            // find Visual object and destroy it - ensuring cleanup
            GameObject visual = GameObject.Find("Visual");
            if (visual != null) Destroy(visual);

            // TODO: ADD AN AWAIT HERE TO ALLOW PLAYERS TO MAKE CORRECTIONS

            // stop shape from moving - switch to stop state
            shape.currentState = shape.StopShape;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // destroy visual object if shape collides with it
        if (other.gameObject.CompareTag("Visual"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bounds") || other.gameObject.CompareTag("Shape"))
        {
            if (transform.parent.GetComponent<ShapeRotator>() == null) return;

            // EDGE CASE (literally lol):
            // if shape is on edge of bounds, skip. This is to prevent rotating the shape when it is on the edge of the bounds
            transform.parent.GetComponent<ShapeRotator>().IncrementRotationSide();
        }
    }
}
