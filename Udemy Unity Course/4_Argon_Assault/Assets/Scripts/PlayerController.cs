using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float ControlSpeed = 15f;
    [Tooltip("In m")] [SerializeField] float xRange = 6f;
    [Tooltip("In ms^-1")] [SerializeField] float yRange= 2.2f;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -5.5f;
    [SerializeField] float positionYawFactor = 4f;
    
    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float controlRollFactor = -30f;
    [SerializeField] GameObject[] guns;

    float xThrow, yThrow;
    bool isControllsEnabled = true;

	// Update is called once per frame
	void Update ()
    {
        if (isControllsEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
        
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetActiveGuns(true);
        }
        else
        {
            SetActiveGuns(false);
        }
    }

    private void SetActiveGuns(bool isActive)
    {
        foreach(GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;

        }
    }


    void OnePlayerDeath()
    {
        isControllsEnabled = false;
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControl;


        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * ControlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ControlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampXPos, clampYPos, transform.localPosition.z);
    }



}
