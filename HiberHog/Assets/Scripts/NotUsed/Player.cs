using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerControls playerControls;

    [Header("Movement")]
    public int speed = 5;
    public int RotSpeed = 20;
    public float DashForce = 1f;
    public Rigidbody rb;

    [Header("Shielding")]
    public int shieldTimer = 1;
    public GameObject hog;
    public GameObject[] shieldHog;


    private BoxCollider boxCol;

    private void Awake()
    {
        playerControls = new PlayerControls();
        boxCol = this.gameObject.GetComponent<BoxCollider>();
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
        bool Shiftkey = playerControls.Player.Dash.ReadValue<float>() > 0.1f;

        if (Shiftkey)
        {
            Dash();
        }

        bool Space = playerControls.Player.Protect.ReadValue<float>() > 0.1f;

        if (Space)
        {
            Protect();
        }


       // float x = playerControls.Player.Horizontal.ReadValue<float>();

        float z = playerControls.Player.Vertical.ReadValue<float>();

        float rotateDirection = playerControls.Player.Rotate.ReadValue<float>();

       // transform.Translate(speed * x * Time.deltaTime, 0, 0);
        transform.Rotate(Vector3.up * Time.deltaTime * RotSpeed * rotateDirection);
        transform.Translate(0, 0, speed * z * Time.deltaTime);

    }

    public void Dash()
    {
        rb.AddForce(transform.forward * DashForce, ForceMode.Impulse);
        
    }

    public void Protect()
    {
        StartCoroutine(Shield());
    }

    IEnumerator Shield()
    {
        

        playerControls.Disable();

        boxCol.size = new Vector3 (2f, 1f, 2.25f);

        hog.SetActive(false);
        for (int i = 0; i < shieldHog.Length; i++)
        {
            shieldHog[i].SetActive(true);
        }

        yield return new WaitForSeconds(shieldTimer);

        boxCol.size = new Vector3(1f, 1f, 2.25f);

        hog.SetActive(true);
        for (int i = 0; i < shieldHog.Length; i++)
        {
            shieldHog[i].SetActive(false);
        }

        playerControls.Enable();

        

    }
}
