using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 40f;
    [SerializeField] float weaponDamage = 5;
    [SerializeField] float fullMag = 30;
    [SerializeField] float currentAmmo = 90;
    public float CurrentAmmo { get { return currentAmmo; } }
    public float rpm = 360;
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject hitEffect;
    [SerializeField] AudioSource gunShot;

    float magAmmo;
    public float MagAmmo { get { return magAmmo; } }

    private void Start()
    {
        magAmmo = fullMag;
    }

    public void Shoot()
    {
        magAmmo--;

        ProcessRaycast();
        PlayMuzzleFlash();
        PlayAudio();
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlashVFX.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();

            if (target != null)
            {
                target.TakeDamage(weaponDamage);
            }
        }
        else
        {
            return;
        }
    }

    private void PlayAudio()
    {
        gunShot.Play();
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1f);
    }

    public void RefillAmmo()
    {
        if (currentAmmo + magAmmo - 30 > 0)
        {
            currentAmmo += magAmmo;
            currentAmmo -= fullMag;
            magAmmo = fullMag;
        }
        else
        {
            magAmmo = magAmmo + currentAmmo;
            currentAmmo = 0;
        }
    }
}
