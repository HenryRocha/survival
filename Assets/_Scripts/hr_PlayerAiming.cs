using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class hr_PlayerAiming : MonoBehaviour
{
    [Header("Aim Settings")]
    [SerializeField] private GameObject aimCamera;
    [SerializeField] private Rig aimRig;
    [SerializeField] private float aimTime;

    [Header("Aim Flags")]
    public bool isAiming = false;
    public bool isFiring = false;
    public bool firingState = false;

    private hr_RaycastWeapon weapon;
    private bool aimState = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        weapon = GetComponentInChildren<hr_RaycastWeapon>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void HandleAllAiming()
    {
        HandleAiming();
        HandleFiring();
    }

    private void HandleAiming()
    {
        if (isAiming)
        {
            aimRig.weight = Mathf.Lerp(aimRig.weight, 1.0f, Time.deltaTime * aimTime);

            if (!aimState)
            {
                aimState = true;
                aimCamera.SetActive(true);
            }
        }
        else
        {
            aimRig.weight = Mathf.Lerp(aimRig.weight, 0.0f, Time.deltaTime * aimTime);
            if (aimState)
            {
                aimState = false;
                aimCamera.SetActive(false);
            }
        }
    }

    private void HandleFiring()
    {
        if (isAiming)
        {
            if (isFiring)
            {
                if (!firingState)
                {
                    firingState = true;
                    weapon.StartFiring();
                }
            }
            else
            {
                if (firingState)
                {
                    firingState = false;
                    weapon.StopFiring();
                }
            }
        }
    }
}
