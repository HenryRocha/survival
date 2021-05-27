using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hr_RaycastWeapon : MonoBehaviour
{
    public bool isFiring = false;

    [Header("Effects")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem fleshEffect;
    [SerializeField] private TrailRenderer tracerEffect;

    [Header("Settings")]
    [SerializeField] private Transform raycastOrigin;

    private Ray ray;
    private RaycastHit hit;

    public void StartFiring()
    {
        isFiring = true;
        muzzleFlash.Emit(1);

        ray.origin = raycastOrigin.position;
        ray.direction = -1.0f * raycastOrigin.forward;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Zombie"))
            {
                fleshEffect.transform.position = hit.point;
                fleshEffect.transform.forward = hit.normal;
                fleshEffect.Emit(1);

                hit.collider.GetComponent<hr_ZombieManager>().TakeDamage();
            }
            else
            {
                hitEffect.transform.position = hit.point;
                hitEffect.transform.forward = hit.normal;
                hitEffect.Emit(1);
            }

            tracer.transform.position = hit.point;
        }
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
