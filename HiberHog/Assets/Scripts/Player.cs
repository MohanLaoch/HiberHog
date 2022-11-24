using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerControls playerControls;

    public int speed = 5;
    public int DashSpeed = 10;

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
        bool Spacekey = playerControls.Player.Dash.ReadValue<float>() > 0.1f;

        if (Spacekey)
        {
            Dash();

        }
        float x = playerControls.Player.Horizontal.ReadValue<float>();

        float z = playerControls.Player.Vertical.ReadValue<float>();

        transform.Translate(speed * x * Time.deltaTime, 0, 0);

        transform.Translate(0, 0, speed * z * Time.deltaTime);

    }

  public void Dash()
    {
        gameObject.transform.Translate(DashSpeed * Time.deltaTime, 0, 0);
        
    }
}
