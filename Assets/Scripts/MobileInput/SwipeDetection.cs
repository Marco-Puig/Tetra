using UnityEngine;
using UnityEngine.Events;

public class Swipe_Controller : MonoBehaviour
{
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    public int number = 0;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;

            if (endTouchPos.x < startTouchPos.x)
            {
                NextSideOfPanel();
            }
            else if (endTouchPos.x > startTouchPos.x)
            {
                PrevSideOfPanel();
            }

            Debug.Log("Swipe detected!");
        }
    }

    void NextSideOfPanel()
    {
        number++;
    }

    void PrevSideOfPanel()
    {
        number--;
    }
}

