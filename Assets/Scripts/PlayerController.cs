using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour{

    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 4f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 4f;
    [Tooltip("In m")] [SerializeField] float xRange = 7f;
    [Tooltip("In m")] [SerializeField] float yRange = 5f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-Position")]
    [SerializeField] float positionPitchFactor = -2.5f;
    [SerializeField] float positionYawFactor = 2.5f;
    [Header("Control-Throw")]
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float controlPitchFactor = -20f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    void Update() {
        if (isControlEnabled) {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
        }
    }

    void OnPlayerDeath() { //Called by string method
        isControlEnabled = false;
    }

    private void ProcessRotation() {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation() {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessFiring() {
        if (CrossPlatformInputManager.GetButton("Fire")) {
            ActivateGuns();
        } else {
            DeactivateGuns();
        }
    }

    private void ActivateGuns() {
        foreach(GameObject gun in guns) {
            gun.SetActive(true);
        }
    }

    private void DeactivateGuns() {
        foreach (GameObject gun in guns) {
            gun.SetActive(false);
        }
    }

}
