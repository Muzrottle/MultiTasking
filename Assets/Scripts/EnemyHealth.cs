using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyHealth = 50f;

    EnemyAI enemyAI;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    public void TakeDamage(float damageDealt)
    {
        enemyHealth -= damageDealt;
        OnDamageTaken?.Invoke(this, null);
        Debug.Log("My Current Health: " + enemyHealth);

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public event EventHandler OnDamageTaken;
}
