using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerController : MonoBehaviour {

    [Header("General")]
    [Tooltip("In meter/s")] [SerializeField] float controlSpeed = 30f;
    [Tooltip("In meters")] [SerializeField] float xRange = 20f;
    [Tooltip("In meters")] [SerializeField] float yRange = 11f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 3.5f;

    [Header("Control-throw based")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -25f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    void Update() {
        if (isControlEnabled) {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }

    }

    private void ProcessTranslation() {
        xThrow = Input.GetAxis("Horizontal");
        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yThrow = Input.GetAxis("Vertical");
        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation() {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring() {
        if (Input.GetButton("Fire1")) {
            SetGunsActive(true);
        } else {
            SetGunsActive(false);
        }

    }

    private void SetGunsActive(bool isActive) {
        foreach (GameObject gun in guns) {
            EmissionModule emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
    private void OnPlayerDeath() { //called by string reference
        isControlEnabled = false;
    }
}
