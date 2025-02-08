using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] private HealthManager healthManagerSO;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeRemaining = 360f;
    private bool isTimerRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (timeRemaining > 0)
        {
            
            timerText.text = timeRemaining.ToString();
            yield return new WaitForSeconds(1f);
            timeRemaining --;
        }
        healthManagerSO.TimeOver();
    }
}
