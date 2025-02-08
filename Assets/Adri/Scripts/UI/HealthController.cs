using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private HealthManager healthManagerSO;
    [SerializeField] private Image healthFillImage;
    [SerializeField] private GameObject HUDScreen;
    [SerializeField] private GameObject DeadScreen;
    [SerializeField] private GameObject VictoryScreen;

    // Start is called before the first frame update
    void Start()
    {
        healthManagerSO.ResetManager();
    }

    private void OnEnable()
    {
        healthManagerSO.OnPlayerDamaged += UpdateHealthFillImage;
        healthManagerSO.OnPlayerDead += LoadDeadScreen;
        healthManagerSO.OnPlayerHealed += UpdateHealthFillImage;
        healthManagerSO.OnPlayerSucceded += LoadVictoryScreen;
    }

    private void OnDisable()
    {
        healthManagerSO.OnPlayerDamaged -= UpdateHealthFillImage;     
        healthManagerSO.OnPlayerDead -= LoadDeadScreen;
        healthManagerSO.OnPlayerHealed -= UpdateHealthFillImage;
        healthManagerSO.OnPlayerSucceded -= LoadVictoryScreen;
    }

    private void UpdateHealthFillImage (float playerHealth)
    {
        healthFillImage.fillAmount = playerHealth;
        
    }

    private void LoadDeadScreen()
    {
        HUDScreen.SetActive (false);
        DeadScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    private void LoadVictoryScreen()
    {
        HUDScreen.SetActive (false);
        VictoryScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
