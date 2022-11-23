using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerControls playerControls;

    public int speed = 5; 

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        float x = playerControls.Player.Horizontal.ReadValue<float>();

        float z = playerControls.Player.Vertical.ReadValue<float>();

        transform.Translate(speed * x * Time.deltaTime, 0, 0);

        transform.Translate(0, 0, speed * z * Time.deltaTime);
    }
}
