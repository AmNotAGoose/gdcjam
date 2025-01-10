using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerCamera;
    public PlayerEffects playerEffects;

    public Transform groundCheck;
    public LayerMask groundMask;
    public LayerMask wallMask;

    public float moveSpeed;
    public float acceleration;
    public float jumpForce;
    public float fallForce;
    public float mouseSensitivity;
    public float groundDistance;
    public float wallDistance;

    public bool isNearWall;
    public bool isGrounded;

    public int maxAirJumps;
    private int curAirJumps;
    public int maxWallJumps;
    private int curWallJumps;

    public float dashForce;
    public float dashCooldown;
    public float dashDuration;
    public float dashDistance;
    public float dashSpeed;
    public float dashEndForce;
    public bool canDash;

    private float xRotation;
    private Vector3 targetVelocity;

    public Vector3 checkpoint;
    public GameObject deathScreen;
    public PlayerStats playerStats;

    public GameObject winPanel;
    public GameObject bossBar;
    public TextMeshProUGUI winText;
    public GameObject healthBar;

    public AudioSource jumpSfx;
    public AudioSource dashSfx;
    public AudioSource walkSfx;


    public bool isIce;
    private float xVelocity;
    private float yVelocity;

    void Start()
    {
        canDash = true;
        dashCooldown = 1.3f;
        moveSpeed = 12;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        GroundCheck();
        CameraControl();
        ProcessInput();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            curWallJumps = 0;
            curAirJumps = 0;
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && (isNearWall && curWallJumps <= maxWallJumps))
        {
            RaycastHit hit;
            if (Physics.SphereCast(groundCheck.position, wallDistance, -transform.forward, out hit, wallDistance + 0.5f, wallMask))
            {
                print(hit);
                Vector3 wallNormal = hit.normal;
                rb.velocity += wallNormal * 2f;
            }
            Jump();
            curWallJumps += 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && (curAirJumps <= maxAirJumps))
        {
            Jump();
            curAirJumps += 1;
        }

        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();

        if (!isGrounded && rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallForce * Time.fixedDeltaTime;
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (!isGrounded)
        {
            isNearWall = Physics.CheckSphere(groundCheck.position, wallDistance, wallMask) | Physics.CheckSphere(groundCheck.position, wallDistance, groundMask);
        }
        foreach (Collider col in Physics.OverlapSphere(groundCheck.position, groundDistance))
        {
            if (col.gameObject.CompareTag("Ice"))
            {
                isIce = true;
                break;
            } else
            {
                isIce = false;
            }
        }
    }

    private void CameraControl()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * PlayerPrefs.GetFloat("sensitivity");
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * PlayerPrefs.GetFloat("sensitivity");

        xVelocity = Mathf.Lerp(xVelocity, mouseX, 3 * Time.deltaTime);
        yVelocity = Mathf.Lerp(yVelocity, mouseY, 3 * Time.deltaTime);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, playerCamera.localRotation.y, playerCamera.localRotation.z);

        transform.Rotate(Vector3.up * mouseX);
    }
    private IEnumerator Dash()
    {
        canDash = false;

        Vector3 dashDirection = new Vector3(rb.velocity.x, 0f, rb.velocity.z).normalized;

        if (dashDirection == Vector3.zero)
        {
            dashDirection = transform.forward;
        }

        playerEffects.PlayDashEffect(dashDirection);

        rb.AddForce(dashDirection * dashForce, ForceMode.VelocityChange);
        dashSfx.Play();
        yield return new WaitForSeconds(dashDuration);

        if (rb.velocity.y == 0)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;

        /*canDash = false;

        Vector3 dashDirection = new Vector3(rb.velocity.x, 0f, rb.velocity.z).normalized; //not where the player is looking, where they are currently going

        if (dashDirection == Vector3.zero)
        {
            dashDirection = transform.forward; //forward if aint moving
        }

        float dashStartTime = Time.time;
        Vector3 dashStartPosition = transform.position;
        Vector3 dashEndPosition = dashStartPosition + dashDirection * dashDistance;

        float totalDistanceTraveled = 0f;

        bool collidedWithSomething = false;

        while (Time.time < dashStartTime + dashDuration && totalDistanceTraveled < dashDistance)
        {   
            transform.position = Vector3.MoveTowards(transform.position, dashEndPosition, dashSpeed * Time.deltaTime);
            totalDistanceTraveled = Vector3.Distance(dashStartPosition, transform.position);
            collidedWithSomething = Physics.CheckSphere(transform.position, wallDistance, wallMask);
            if (collidedWithSomething)
            {
                break;
            }
            yield return null;
        }

        // tp player to end if somehow not already there
        if (!collidedWithSomething && totalDistanceTraveled < dashDistance)
        {
            transform.position = dashEndPosition;
        }

     //   float dashBoostTime = 0f;

     //   while (dashBoostTime < dashDuration)
     //   {
     //       rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, dashEndForce * Time.deltaTime);
     //       dashBoostTime += Time.deltaTime;
    //        yield return null;
     //   }

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;*/
    }

    private void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        targetVelocity = (transform.right * moveX + transform.forward * moveZ).normalized * moveSpeed;
    }

    private void ApplyMovement()
    {
        Vector3 currentVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Vector3 velocityChange = (targetVelocity - currentVelocity) * acceleration * Time.fixedDeltaTime;

        if (velocityChange != Vector3.zero && !walkSfx.isPlaying)
        {
            walkSfx.Play();
        }

        if (!isIce)
        {
            if (targetVelocity == Vector3.zero)
            {
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            }
            else
            {
                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }
        } else
        {
            rb.AddForce(targetVelocity * 5, ForceMode.Force);
        }
    }

    private void Jump()
    {
        jumpSfx.Play();
        playerEffects.PlayJumpEffect();
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    public void Die()
    {
        healthBar.SetActive(false);
        deathScreen.SetActive(true);
    }
    public void Respawn()
    {
        deathScreen.SetActive(false);
    }

    public void Win()
    {
        winPanel.SetActive(true);
        bossBar.SetActive(false);
        playerStats.health = 100000;
        winText.text = "LEVEL CLEARED\nKILLS: " + playerStats.kills.ToString() + "\nTIME: " + Time.realtimeSinceStartup.ToString() + "\n\nPRESS [R] TO CONTINUE";
    }
}
