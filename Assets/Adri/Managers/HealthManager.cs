using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu (fileName = "HealtManager", menuName = "Managers/HealthManager")]
public class HealthManager : ScriptableObject
{
    public event Action<float> OnPlayerDamaged;
    public event Action<float> OnPlayerHealed;
    public event Action OnPlayerDamagedSound;
    public event Action OnPlayerHealedSound;
    public event Action OnPlayerDead;
    public event Action OnPlayerSucceded;
    public event Action OnPlayerPaused;
    public event Action OnPlayerResumed;


    private float playerHealth = 1f;
    private float damageValue = 0.05f;
    private float healthValue = 0.20f;

    public void PlayerDamaged()
    {
        if (playerHealth <= 0f)
        {
            Debug.Log ("Has muerto");
            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            OnPlayerDead?.Invoke();   
        }

        playerHealth -= damageValue;
        Debug.Log("Current Health: " + playerHealth);
        OnPlayerDamaged?.Invoke(playerHealth);
        OnPlayerDamagedSound?.Invoke();
    }

    public void TimeOver()
    {
        Debug.Log("Se ha acabado el tiempo. El jugador pierde.");
        OnPlayerDead?.Invoke();
    }

    public void PlayerHealed()
    {
        if (playerHealth <= 1f)
        {
            Debug.Log ("Te has curado");
            playerHealth += healthValue;
            OnPlayerHealed?.Invoke(playerHealth);
            OnPlayerHealedSound?.Invoke();

            if(playerHealth > 1f)
            {
                playerHealth = 1f;
            }           
            Debug.Log("Current Health: " + playerHealth);
        }

 
    }

    public void PauseScreen()
    {
        Debug.Log("Has pausado el juego");
        OnPlayerPaused?.Invoke();

    }

    public void ResumeGame()
    {
        Debug.Log("Has reanudado el juego");
        OnPlayerResumed?.Invoke();
    }
    public void Success()
    {
        OnPlayerSucceded?.Invoke();
    }

    public void ResetManager()
    {
        playerHealth = 1f;
    }
}
