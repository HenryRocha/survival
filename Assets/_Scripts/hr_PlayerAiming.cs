using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hr_PlayerAiming : MonoBehaviour
{
    [Header("Aim Settings")]
    [SerializeField] private GameObject aimCamera;

    [Header("Aim Flags")]
    public bool isAiming = false;
    public bool isFiring = false;

    private hr_RaycastWeapon weapon;
    private bool firingState = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        weapon = GetComponentInChildren<hr_RaycastWeapon>();
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
            aimCamera.SetActive(true);
        }
        else
        {
            aimCamera.SetActive(false);
        }
    }

    private void HandleFiring()
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
