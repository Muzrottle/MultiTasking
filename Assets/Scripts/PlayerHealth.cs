using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth = 160;
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image healthImg;
    
    DeathHandler deathHandler;

    private void Start()
    {
        deathHandler = GetComponent<DeathHandler>();
        slider.maxValue = playerHealth;
        slider.value = playerHealth;
        healthImg.color = gradient.Evaluate(1f);
    }

    public void PlayerHit(float damage)
    {
        playerHealth -= damage;

        slider.value = playerHealth;
        healthImg.color = gradient.Evaluate(slider.normalizedValue);

        if (playerHealth <= 0)
        {
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        deathHandler.HandleDeath();
    }
}
