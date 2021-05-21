using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hr_PlayerManager : MonoBehaviour
{
    private hr_InputManager inputManager;
    private hr_PlayerLocomotion playerLocomotion;
    private hr_PlayerAiming playerAiming;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        inputManager = GetComponent<hr_InputManager>();
        playerLocomotion = GetComponent<hr_PlayerLocomotion>();
        playerAiming = GetComponent<hr_PlayerAiming>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        inputManager.HandleAllInputs();
        playerAiming.HandleAllAiming();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }
}
