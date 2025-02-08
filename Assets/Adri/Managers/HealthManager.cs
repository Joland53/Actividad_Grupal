using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu (fileName = "HealtManager", menuName = "Managers/HealthManager")]
public class HealthManager : ScriptableObject
{
    public event Action<float> OnPlayerDamaged;
    public event Action<float> OnPlayerHealed;
    public event Action OnPlayerDamagedSound;
    public event Action OnPlayerDead;


    private float playerHealth = 1f;
    private float damageValue = 0.05f;
    private float healthValue = 0.50f;

    public void PlayerDamaged()
    {
        if (playerHealth <= 0)
        {
            Debug.Log ("Has muerto");   
            OnPlayerDead?.Invoke();   
        }

        playerHealth -= damageValue;
        Debug.Log("Current Health: " + playerHealth);
        OnPlayerDamaged?.Invoke(playerHealth);
        OnPlayerDamagedSound?.Invoke();
    }

    public void PlayerHealed()
    {
        if (playerHealth < 1)
        {
            Debug.Log ("Te has curado");
            playerHealth += healthValue;
            Debug.Log("Current Health: " + playerHealth);
            OnPlayerHealed?.Invoke(playerHealth);
        }

    }
    
    public void ResetManager()
    {
        playerHealth = 1f;
    }
}
