using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private HealthManager healthManagerSO;
    [SerializeField] private ScoreManager scoreManagerSO;
    [SerializeField] private SoundManager soundManagerSO;

    private AudioSource audioSource;
    [SerializeField] private AudioClip collectedCoinSound;
    [SerializeField] private AudioClip deadEnemy;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip healedPlayerSound;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        soundManagerSO.OnDeadEnemy += DeadEnemySound;
        healthManagerSO.OnPlayerHealedSound += HealedPlayerSound;
        scoreManagerSO.OnCollectedCoinSound += CollectedCoinSound;
        soundManagerSO.OnShootSound += ShootSound;
        
    }

    private void OnDisable()
    {
        soundManagerSO.OnDeadEnemy -= DeadEnemySound;
        healthManagerSO.OnPlayerHealedSound -= HealedPlayerSound;
        scoreManagerSO.OnCollectedCoinSound -= CollectedCoinSound;
        soundManagerSO.OnShootSound -= ShootSound;        
    }
    
    private void DeadEnemySound()
    {
        if (audioSource != null && deadEnemy != null)
        {
            audioSource.PlayOneShot(deadEnemy);
        }
    }

    private void HealedPlayerSound()
    {
        if (audioSource != null && healedPlayerSound != null)
        {
            audioSource.PlayOneShot(healedPlayerSound);
        }
    }

    private void CollectedCoinSound()
    {
        if (audioSource != null && collectedCoinSound != null)
        {
            audioSource.PlayOneShot(collectedCoinSound);
        }
    }

    private void ShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
    
}
