using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth = 160;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
