using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffUI : MonoBehaviour
{
    public GameObject UI;

    public GameObject[] cameras;

    public void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            UI.SetActive(false);
        }

        if (Input.GetKeyDown("p"))
        {
            UI.SetActive(true);
        }

        if (Input.GetKeyDown("1"))
        {
            foreach (GameObject camera in cameras)
            {
                camera.SetActive(false);
            }

            cameras[0].SetActive(true);
        }

        if (Input.GetKeyDown("2"))
        {
            foreach (GameObject camera in cameras)
            {
                camera.SetActive(false);
            }

            cameras[1].SetActive(true);
        }

        if (Input.GetKeyDown("3"))
        {
            foreach (GameObject camera in cameras)
            {
                camera.SetActive(false);
            }

            cameras[2].SetActive(true);
        }

        if (Input.GetKeyDown("4"))
        {
            foreach (GameObject camera in cameras)
            {
                camera.SetActive(false);
            }

            cameras[3].SetActive(true);
        }
    }
}
