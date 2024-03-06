using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("Camera Attributes")]
    [SerializeField] Camera FPCamera;
    [SerializeField] CinemachineVirtualCamera FPCameraCinemachine;
    [SerializeField] float zoomFOV = 30f;
    [SerializeField] float zoomDuration = 1.5f;
    float defaultFOV;

    [Header("Weapon Attributes")]
    [SerializeField] float range = 40f;
    [SerializeField] float weaponDamage = 5;
    [SerializeField] float fullMag = 30;
    public float FullMag { get { return fullMag; } }
    [SerializeField] float currentAmmo = 90;
    public float CurrentAmmo { get { return currentAmmo; } }
    public float rpm = 360;
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject hitEffect;
    [SerializeField] AudioSource gunShot;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI ammoText;
    
    float magAmmo;
    public float MagAmmo { get { return magAmmo; } }

    private void Start()
    {
        magAmmo = fullMag;
        defaultFOV = FPCameraCinemachine.m_Lens.FieldOfView;
        
        UpdateAmmoUI();
    }

    public void Shoot()
    {
        magAmmo--;

        UpdateAmmoUI();
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

        UpdateAmmoUI();
    }

    public void AimZoom(bool isAiming)
    {
        if (isAiming)
        {
            Debug.Log("Girdim");
            DOTween.To(() => FPCameraCinemachine.m_Lens.FieldOfView, x => FPCameraCinemachine.m_Lens.FieldOfView = x, zoomFOV, zoomDuration)
            .SetEase(Ease.Linear);
        }
        else
        {
            Debug.Log("Çýktým");

            DOTween.To(() => FPCameraCinemachine.m_Lens.FieldOfView, x => FPCameraCinemachine.m_Lens.FieldOfView = x, defaultFOV, zoomDuration)
            .SetEase(Ease.Linear);
        }
    }

    public void UpdateAmmoUI()
    {
        ammoText.text = $"{magAmmo} | {currentAmmo}";
    }
}
