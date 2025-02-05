using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "HealtManager", menuName = "Managers/HealthManager")]
public class HealthManager : ScriptableObject
{
    public event Action<float> OnPlayerDamaged;
    public event Action OnPlayerDamagedSound;

    private float playerHealth = 1f;
    private float damageValue = 0.05f;

    public void PlayerDamaged()
    {
        if (playerHealth <= 0)
        {
            Debug.Log ("Has muerto");
            /*
                SceneManager.LoadScene (GAMEOVERSCENE);
            */
        }

        playerHealth -= damageValue;
        Debug.Log("Current Health: " + playerHealth);
        OnPlayerDamaged?.Invoke(playerHealth);
        OnPlayerDamagedSound?.Invoke();
    }

    public void ResetManager()
    {
        playerHealth = 1f;
    }
}
