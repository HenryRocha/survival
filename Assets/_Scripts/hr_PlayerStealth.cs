using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hr_PlayerStealth : MonoBehaviour
{
    [Header("Sound Intensity")]
    [SerializeField] private LayerMask zombieLayer;
    [SerializeField] private float shootSoundIntensity = 12.0f;
    [SerializeField] private float baseStealthProfile = 1.5f;
    [SerializeField] private float sprintSoundIntensity = 3f;
    [SerializeField] private float walkSoundIntensity = 2f;

    private hr_PlayerLocomotion playerLocomotion;
    private hr_PlayerAiming playerAiming;
    private SphereCollider sphereCollider;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        playerLocomotion = GetComponentInParent<hr_PlayerLocomotion>();
        playerAiming = GetComponentInParent<hr_PlayerAiming>();
        sphereCollider = GetComponentInChildren<SphereCollider>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        AlertNearbyZombies();

        sphereCollider.radius = baseStealthProfile * GetPlayerStealthProfile();
    }

    /// <summary>
    /// Creates a sphere around the player, with the radius being the shootSoundIntensity.
    /// All the zombies inside this sphere will become aware of the player.
    /// </summary>
    private void AlertNearbyZombies()
    {
        if (playerAiming.firingState)
        {
            Collider[] zombies = Physics.OverlapSphere(transform.position, shootSoundIntensity, zombieLayer);

            foreach (var zombie in zombies)
            {
                zombie.GetComponent<hr_ZombieManager>().OnAware();
            }
        }
    }

    /// <summary>
    /// Returns the stealth profile of the player.
    /// </summary>
    private float GetPlayerStealthProfile()
    {
        if (playerLocomotion.isWalking)
        {
            if (playerLocomotion.isSprinting)
            {
                return 3;
            }
            else
            {
                return 2;
            }
        }
        else
        {
            return 1;
        }
    }
}
