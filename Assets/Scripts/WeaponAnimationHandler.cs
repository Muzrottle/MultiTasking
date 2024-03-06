using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class WeaponAnimationHandler : MonoBehaviour
{
    Animator playerAnimator;
    Weapon weapon;
    private StarterAssetsInputs input;

    float timeSinceLastShot;

    bool isWalking = false;
    bool isReloading = false;
    
    int reloadLayerIndex = 3;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
        playerAnimator = GetComponent<Animator>();
        input = GetComponentInParent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (playerAnimator == null)
            return;
        
        PlayerInput();

        if (isReloading)
        {
            if (IsSprinting())
            {
                playerAnimator.Play("Reloading.Idle", reloadLayerIndex, 0f);
                isReloading = false;
            }
        }
    }

    private void PlayerInput()
    {
        if (Input.GetButton("Fire1") && CanShoot())
        {
            Debug.Log("Is shooting.");
            Fire(true);
        }
        else if (!Input.GetButton("Fire1") && playerAnimator.GetBool("isShooting"))
        {
            Debug.Log("Also not.");
            Fire(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Aim(true);
            weapon.AimZoom(true);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            Aim(false);
            weapon.AimZoom(false);
        }

        if (input.move != Vector2.zero)
        {
            Walk();
        }
        else
        {
            isWalking = false;

            playerAnimator.SetBool("isSprinting", false);
            playerAnimator.SetBool("isWalking", isWalking);
        }
    }

    private bool CanShoot() => !isReloading && timeSinceLastShot > 1f / (weapon.rpm / 60f);
    
    private bool CanReload() => weapon.CurrentAmmo != 0 && weapon.MagAmmo != weapon.FullMag;

    private bool IsSprinting() => input.sprint;

    private void Fire(bool isShooting)
    {
        if (isShooting)
        {
            if (weapon.MagAmmo == 0 && weapon.CurrentAmmo == 0)
            {
                return;
            }
            else if (weapon.MagAmmo == 0)
            {
                Reload();
                return;
            }

            weapon.Shoot();
            playerAnimator.SetFloat("magAmmo", weapon.MagAmmo);

            timeSinceLastShot = 0f;

            Debug.Log(weapon.MagAmmo);

            if (weapon.MagAmmo == 0)
            {
                Reload();
            }
        }

        playerAnimator.SetBool("isShooting", isShooting);
    }

    private void Reload()
    {
        if (!CanReload())
            return;

        isReloading = true;

        if (weapon.MagAmmo == 0)
        {
            playerAnimator.Play("ReloadNoAmmo");
        }
        else
        {
            playerAnimator.Play("Reload");
        }
        
    }

    //Called from reload animation.
    private void Reloaded()
    {
        isReloading = false;
        weapon.RefillAmmo();
        Debug.Log(weapon.CurrentAmmo);
    }

    private void Aim(bool isAiming)
    {
        playerAnimator.SetBool("isSprinting", false);
        playerAnimator.SetBool("isWalking", false);
        
        playerAnimator.SetBool("isAiming", isAiming);
    }

    private void Walk()
    {
        isWalking = true;

        playerAnimator.SetBool("isWalking", isWalking);

        if (IsSprinting())
        {
            playerAnimator.SetBool("isSprinting", true);
        }
        else
        {
            playerAnimator.SetBool("isSprinting", false);
        }
    }
}
