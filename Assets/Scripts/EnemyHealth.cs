using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyHealth = 50f;

    public void TakeDamage(float damageDealt)
    {
        enemyHealth -= damageDealt;
        Debug.Log("My Current Health: " + enemyHealth);

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
