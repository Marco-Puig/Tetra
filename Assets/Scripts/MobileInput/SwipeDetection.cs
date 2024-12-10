using UnityEngine;
using UnityEngine.Events;

public class Swipe_Controller : MonoBehaviour
{
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;

    void Update()
    {
		// Touch phase - Began: When the user first touches the screen
		// Touch phase - Ended: When the user lifts their finger off the screen
		// If the position of the touch position is different, then the user has swiped

		// If touch is detected, store the start and end positions of the touch
		// Check the touch state, so at the start of the touch state, the start position is stored
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }

		// Check the touch state, so at the end of the touch state, the end position is stored
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;

			// ensure that the swipe is long enough to be considered a swipe
			if (Vector2.Distance(startTouchPos, endTouchPos) < 50f)
			{
				return;
			}

			// check that panels are not currently rotating
			if (PanelManager.instance.currentState != PanelManager.instance.RotateOnInput)
			{
				return;
			}

            if (endTouchPos.x < startTouchPos.x)
            {
                // right swipe
				PanelManager.instance.currentState = () => PanelManager.instance.RotatePanel(Vector3.up);
            }
            else if (endTouchPos.x > startTouchPos.x)
            {
               // left swipe
			   PanelManager.instance.currentState = () => PanelManager.instance.RotatePanel(Vector3.down);
            }
        }
    }
}

