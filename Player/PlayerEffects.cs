using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEffects : MonoBehaviour
{
    public Player player;

    public Camera playerCamera;
    public float tiltAmount;
    public float tiltSpeed;

    public float targetTilt;
    public float currentTilt;

    public GameObject dashEffectContainer;
    public ParticleSystem dashEffect;

    public GameObject jumpEffectContainer;
    public ParticleSystem jumpEffect;

    public Slider slider;

    void Start()
    {
        playerCamera = Camera.main;
        player = GetComponent<Player>();
    }

    void Update()
    {
        slider.value = player.playerStats.health;

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0)
        {
            targetTilt = tiltAmount;
        }
        else if (horizontalInput < 0)
        {
            targetTilt = -tiltAmount;
        }
        else
        {
            targetTilt = 0f;
            playerCamera.transform.localRotation = Quaternion.Euler(playerCamera.transform.localRotation.eulerAngles.x, playerCamera.transform.localRotation.eulerAngles.y, 0f);
        }

        currentTilt = Mathf.Lerp(currentTilt, targetTilt, tiltSpeed * Time.deltaTime);

        playerCamera.transform.localRotation = Quaternion.Euler(playerCamera.transform.localRotation.eulerAngles.x, playerCamera.transform.localRotation.eulerAngles.y, currentTilt);
    }

    public void PlayDashEffect(Vector3 dashDirection)
    {
        dashEffectContainer.transform.rotation = Quaternion.LookRotation(dashDirection);
        dashEffectContainer.transform.position = player.transform.position + dashDirection * 7f;
        dashEffect.Play();
    }

    public void PlayJumpEffect()
    {
        jumpEffectContainer.transform.position = player.transform.position;
        jumpEffect.Play();
    }
}
