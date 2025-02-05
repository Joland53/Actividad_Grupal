using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private HealthManager healthManagerSO;
    [SerializeField] private Image healthFillImage;

    // Start is called before the first frame update
    void Start()
    {
        healthManagerSO.ResetManager();
    }

    private void OnEnable()
    {
        healthManagerSO.OnPlayerDamaged += UpdateHealthFillImage;
        
    }

    private void OnDisable()
    {
        healthManagerSO.OnPlayerDamaged -= UpdateHealthFillImage;        
    }

    private void UpdateHealthFillImage (float playerHealth)
    {
        healthFillImage.fillAmount = playerHealth;
    }
}
