using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private PlayerControls playerControls;

    [Header("Movement")]
    public Rigidbody rb;
    public int speed = 5;
    public int RotSpeed = 20;
    public float DashForce = 1f;
    public float dashCooldownTime;
    private float dashCooldownTimer;
    private bool dashIsCooldown = false;

    [Header("Shielding")]
    public int shieldTimer = 1;
    public float shieldCooldownTime;
    private float shieldCooldownTimer;
    public float knockBack = 20f;
    public GameObject hog;
    public GameObject[] shieldHog;

    private bool shieldIsCooldown = false;

    [HideInInspector]
    public bool isShielding = false;

    private BoxCollider boxCol;

    [Header("AbilityUI")]
    public Image shieldCooldown;
    public Image dashCooldown;
    public TMP_Text shieldText;
    public TMP_Text dashText;

    public void RestartScene()
    {
        SceneManager.LoadScene("MarkScene");
    }

    private void Start()
    {
        shieldText.gameObject.SetActive(false);
        shieldCooldown.fillAmount = 0.0f;
        dashText.gameObject.SetActive(false);
        dashCooldown.fillAmount = 0.0f;

        playerControls.FlippedOver.Disable();

    }

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
        bool Reset = playerControls.Player.Reset.ReadValue<float>() > 0.1f;

        if (Reset)
        {
            RestartScene();
        }


        bool Shiftkey = playerControls.Player.Dash.ReadValue<float>() > 0.1f;

        if (Shiftkey)
        {
            Dash();
        }

        if (dashIsCooldown)
        {
            ApplyDashCooldown();
        }

        bool Space = playerControls.Player.Protect.ReadValue<float>() > 0.1f;

        if (Space)
        {
            Protect();
        }

        if (shieldIsCooldown)
        {
            ApplyShieldCooldown();
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

        if (dashIsCooldown)
        {
            return;
        }
        else
        {
            dashIsCooldown = true;
            dashText.gameObject.SetActive(true);
            dashCooldownTimer = dashCooldownTime;

            rb.AddForce(transform.forward * DashForce, ForceMode.Impulse);
        }
        
    }

    public void Protect()
    {
        if (shieldIsCooldown)
        {
            return;
        }
        else
        {
            shieldIsCooldown = true;
            shieldText.gameObject.SetActive(true);
            shieldCooldownTimer = shieldCooldownTime;

            StartCoroutine(Shield());
            FindObjectOfType<AudioManager>().Play("Shield");
        }
    }

    void ApplyShieldCooldown()
    {
        shieldCooldownTimer -= Time.deltaTime;

        if(shieldCooldownTimer < 0.0f)
        {
            shieldIsCooldown = false;
            shieldText.gameObject.SetActive(false);
            shieldCooldown.fillAmount = 0.0f;
        }
        else
        {
            shieldText.text = Mathf.RoundToInt(shieldCooldownTimer).ToString();
            shieldCooldown.fillAmount = shieldCooldownTimer / shieldCooldownTime;
        }
    }

    void ApplyDashCooldown()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (dashCooldownTimer < 0.0f)
        {
            dashIsCooldown = false;
            dashText.gameObject.SetActive(false);
            dashCooldown.fillAmount = 0.0f;
        }
        else
        {
            dashText.text = Mathf.RoundToInt(dashCooldownTimer).ToString();
            dashCooldown.fillAmount = dashCooldownTimer / dashCooldownTime;
        }
    }

    IEnumerator Shield()
    {
        isShielding = true;

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

        isShielding = false;
    }
}
