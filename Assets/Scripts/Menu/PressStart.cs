using System;
using UnityEditor;
using UnityEngine;

// script for when you press any key, you are transitioned to the main menu
public class PressStart : MonoBehaviour
{

    [SerializeField] GameObject title; // title ui element
    [SerializeField] GameObject cam; // move smoothly up to the main menu
    [SerializeField] GameObject[] showElements;
    [SerializeField] GameObject[] hideElements;

    Action state;
    void Start()
    {
        state = WaitForInput;
    }

    void Update()
    {
        state?.Invoke();
    }

    // wait for any key to be pressed, once pressed, show the main menu and the script is essentially done
    void WaitForInput()
    {
        if (Input.anyKey)
        {
            state = MenuAnimation;

            // hide and show elements respectively
            foreach (GameObject element in showElements)
            {
                element.SetActive(true);
                element.transform.position = Vector3.Lerp(element.transform.position, new Vector3(element.transform.position.x, 900f, element.transform.position.z), 1.25f * Time.deltaTime);
            }
            foreach (GameObject element in hideElements)
            {
                element.SetActive(false);
            }
        }
    }

    // play menu animation to show the full main menu screen / buttons
    void MenuAnimation()
    {
        // move camera up to the main menu slowly and play ui animations
        // TODO: UI ANIMATIONS HERE
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(17f, 0, 0), 1.25f * Time.deltaTime);
        // title.transform.position = Vector3.Lerp(title.transform.position, new Vector3(title.transform.position.x, 1450f, title.transform.position.z), 1.25f * Time.deltaTime);
        if (cam.transform.rotation.x == 17f) state = null; // once camera is at the right angle, stop the script
    }
}
