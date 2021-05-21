using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hr_RaycastWeapon : MonoBehaviour
{
    public bool isFiring = false;

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private TrailRenderer tracerEffect;
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
            Debug.Log($"HIT {hit.point}");

            hitEffect.transform.position = hit.point;
            hitEffect.transform.forward = hit.normal;
            hitEffect.Emit(1);

            tracer.transform.position = hit.point;
        }
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
