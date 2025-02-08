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
    private float healthValue = 20f;

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
        if (playerHealth <= 100)
        {
            Debug.Log ("Te has curado");
        }

        playerHealth += healthValue;
        OnPlayerHealed?.Invoke(playerHealth);
    }
    
    public void ResetManager()
    {
        playerHealth = 1f;
    }
}
