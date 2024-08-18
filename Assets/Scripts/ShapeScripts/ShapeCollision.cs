using UnityEngine;

// ONLY ATTACH THIS SCRIPT TO BOTTOM PIECES OF THE SHAPE

public class ShapeCollision : MonoBehaviour
{
    [SerializeField] Shape shape;
    private void Start()
    {
        // get shape component from parent or current object if current object is parent
        shape = transform.GetComponent<Shape>() != null ? GetComponent<Shape>() : GetComponentInParent<Shape>();
    }

    private void Update()
    {
        // if cube is stopped, return
        if (shape.currentState == shape.StopShape) return;

        // check if cube detects shape or ground layer below it, stop cube from moving
        if (shape.CheckCollision(Vector3.down, transform))
        {
            // stop cube from moving - switch to stop state
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
    }
}
