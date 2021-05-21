using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hr_PlayerLocomotion : MonoBehaviour
{
    [Header("Movement Flags")]
    public bool isWalking = false;
    public bool isSprinting = false;
    public bool isJumping = false;

    [Header("Movement Settings")]
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private float jumpHeight = 10.0f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Animation Settings")]
    [SerializeField] private float blendTime = 0.2f;

    private hr_InputManager inputManager;
    private CharacterController characterController;
    private Animator animator;
    private Transform mainCamera;

    private Vector3 rootMotion;
    private Vector3 velocity;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        inputManager = GetComponent<hr_InputManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (isWalking)
        {
            if (isSprinting)
            {
                UpdateAnimatorSpeedValues(1f, 0f);
            }
            else
            {
                UpdateAnimatorSpeedValues(0.5f, 0f);
            }
        }
        else
        {
            UpdateAnimatorSpeedValues(0f, 0f);
        }

        if (!characterController.isGrounded)
        {
            velocity.y -= gravity;
            characterController.Move(velocity * Time.deltaTime);
        }

        characterController.Move(rootMotion);
        rootMotion = Vector3.zero;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = mainCamera.forward * inputManager.moveAmount.y + mainCamera.right * inputManager.moveAmount.x;
        targetDirection.Normalize();
        targetDirection.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    /// <summary>
    /// Callback for processing animation movements for modifying root motion.
    /// </summary>
    private void OnAnimatorMove()
    {
        rootMotion += animator.deltaPosition;
    }

    private void UpdateAnimatorSpeedValues(float speedZ, float speedX)
    {
        animator.SetFloat("speedZ", speedZ, blendTime, Time.deltaTime);
        animator.SetFloat("speedX", speedX, blendTime, Time.deltaTime);
    }
}
