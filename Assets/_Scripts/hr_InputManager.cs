using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hr_InputManager : MonoBehaviour
{
    public Vector2 moveAmount = Vector2.zero;

    private PlayerControls playerControls;
    private hr_PlayerLocomotion playerLocomotion;
    private hr_PlayerAiming playerAiming;
    private Vector2 movementInput = Vector2.zero;
    private bool sprintInput = false;
    private bool jumpInput = false;
    private bool aimInput = false;
    private bool fireInput = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerLocomotion = GetComponent<hr_PlayerLocomotion>();
        playerAiming = GetComponent<hr_PlayerAiming>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Locomotion.performed += i => movementInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;

            playerControls.PlayerActions.Jump.performed += i => jumpInput = true;
            playerControls.PlayerActions.Jump.canceled += i => jumpInput = false;

            playerControls.PlayerActions.Aim.performed += i => aimInput = true;
            playerControls.PlayerActions.Aim.canceled += i => aimInput = false;

            playerControls.PlayerActions.Fire.performed += i => fireInput = true;
            playerControls.PlayerActions.Fire.canceled += i => fireInput = false;
        }

        playerControls.Enable();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintInput();
        HandleJumpInput();
        HandleAimInput();
        HandleFireInput();
    }

    private void HandleMovementInput()
    {
        if (movementInput != Vector2.zero)
        {
            moveAmount = movementInput;
            playerLocomotion.isWalking = true;
        }
        else
        {
            moveAmount = Vector2.zero;
            playerLocomotion.isWalking = false;
        }
    }

    private void HandleSprintInput()
    {
        if (sprintInput && playerLocomotion.isWalking)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            playerLocomotion.isJumping = true;
        }
    }

    private void HandleAimInput()
    {
        if (aimInput)
        {
            playerAiming.isAiming = true;
        }
        else
        {
            playerAiming.isAiming = false;
        }
    }

    private void HandleFireInput()
    {
        if (fireInput)
        {
            playerAiming.isFiring = true;
        }
        else
        {
            playerAiming.isFiring = false;
        }
    }
}
