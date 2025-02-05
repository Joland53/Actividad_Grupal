using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private HealthManager healthManagerSO;
    [SerializeField] private ScoreManager scoreManagerSO;
    private AudioSource audioSource;
    [SerializeField] private AudioClip collectedCoinSound;
    [SerializeField] private AudioClip damagedPlayerSound;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        healthManagerSO.OnPlayerDamagedSound += DamagedPlayerSound;
        scoreManagerSO.OnCollectedCoinSound += CollectedCoinSound;
        
    }

    private void OnDisable()
    {
        healthManagerSO.OnPlayerDamagedSound -= DamagedPlayerSound;
        scoreManagerSO.OnCollectedCoinSound -= CollectedCoinSound;        
    }
    
    private void DamagedPlayerSound()
    {
        if (audioSource != null && damagedPlayerSound != null)
        {
            audioSource.PlayOneShot(damagedPlayerSound);
        }
    }

    private void CollectedCoinSound()
    {
        if (audioSource != null && collectedCoinSound != null)
        {
            audioSource.PlayOneShot(collectedCoinSound);
        }
    }
    
}
