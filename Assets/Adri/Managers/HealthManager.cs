using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "HealtManager", menuName = "Managers/HealthManager")]
public class HealthManager : ScriptableObject
{
    public event Action<float> OnPlayerDamaged;
    // Start is called before the first frame update

    private float playerHealth = 1f;
    private float damageValue = 0.05f;

    public void PlayerDamaged()
    {
        playerHealth -= damageValue;
        Debug.Log("Current Health: " + playerHealth);
        OnPlayerDamaged?.Invoke(playerHealth);
    }

    public void ResetManager()
    {
        playerHealth = 1f;
    }
}
