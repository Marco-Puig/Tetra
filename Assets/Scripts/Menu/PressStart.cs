using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// script for when you press any key, you are transitioned to the main menu
public class PressStart : MonoBehaviour
{
    [SerializeField] GameObject[] showElements;
    [SerializeField] GameObject[] hideElements;
    [SerializeField] GameObject cam; // move smoothly up to the main menu
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
            state = ShowMenu;
        }
    }

    // play menu animation to show the full main menu screen / buttons
    void ShowMenu()
    {
        // hide and show elements respectively
        foreach (GameObject element in showElements)
        {
            element.SetActive(true);
        }
        foreach (GameObject element in hideElements)
        {
            element.SetActive(false);
        }

        // move camera up to the main menu slowly and play ui animations
        // TODO: UI ANIMATIONS HERE
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(10.0f, 0, 0), 1.25f * Time.deltaTime);
        if (cam.transform.rotation.x == 10f) state = null; // once camera is at the right angle, stop the script
    }
}
