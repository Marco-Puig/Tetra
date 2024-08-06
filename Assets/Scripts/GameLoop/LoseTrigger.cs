using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
            // restart the scene for now
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
