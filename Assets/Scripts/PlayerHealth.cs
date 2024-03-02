using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth = 160;
    
    DeathHandler deathHandler;

    private void Start()
    {
        deathHandler = GetComponent<DeathHandler>();
    }

    public void PlayerHit(float damage)
    {
        playerHealth -= damage;
        Debug.Log(playerHealth);

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
