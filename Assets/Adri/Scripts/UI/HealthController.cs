using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private HealthManager healthManagerSO;
    [SerializeField] private Image healthFillImage;
    [SerializeField] private GameObject DeadScreen;
    [SerializeField] private GameObject HUDScreen;

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
    }

    private void OnDisable()
    {
        healthManagerSO.OnPlayerDamaged -= UpdateHealthFillImage;     
        healthManagerSO.OnPlayerDead -= LoadDeadScreen;
        healthManagerSO.OnPlayerHealed -= UpdateHealthFillImage;
    }

    private void UpdateHealthFillImage (float playerHealth)
    {
        healthFillImage.fillAmount = playerHealth;
        
    }

    private void LoadDeadScreen()
    {
        HUDScreen.SetActive (false);
        DeadScreen.SetActive(true);
    }
}
