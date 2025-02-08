using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationsController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManagerSO;
    [SerializeField] private GameObject deadEnemyMessage;
    [SerializeField] private GameObject pickedUpCoinMessage;

    // Start is called before the first frame update
    void Start()
    {
        scoreManagerSO.ResetManager();
    }

    private void OnEnable()
    {
        scoreManagerSO.OnDeadEnemyMSG += ShowDeadEnemyMessage;
        scoreManagerSO.OnPickedUpCoinMSG += ShowPickedUpCoinMessage;
    }

    private void OnDisable()
    {
        scoreManagerSO.OnDeadEnemyMSG -= ShowDeadEnemyMessage;
        scoreManagerSO.OnPickedUpCoinMSG -= ShowPickedUpCoinMessage;        
    }
    
    public void ShowDeadEnemyMessage()
    {
        StartCoroutine (ShowDeadEnemyMessageForDuration(2f));
    }
    private IEnumerator ShowDeadEnemyMessageForDuration (float duration)
    {
        deadEnemyMessage.SetActive(true);
        yield return new WaitForSeconds (duration);
        deadEnemyMessage.SetActive(false);
    }

    public void ShowPickedUpCoinMessage()
    {
        StartCoroutine (ShowDPickedUpCoinForDuration(2f));
    }
    private IEnumerator ShowDPickedUpCoinForDuration (float duration)
    {
        pickedUpCoinMessage.SetActive(true);
        yield return new WaitForSeconds (duration);
        pickedUpCoinMessage.SetActive(false);
    }

    
}
