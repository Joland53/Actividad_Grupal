using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ScoreManager", menuName = "Managers/ScoreManager")]
public class ScoreManager : ScriptableObject
{

    public event Action<int> OnPickedUpCoin;
    public event Action<int> UpdateScore;
    public event Action OnCollectedCoinSound;
    public event Action OnDeadEnemy;

    private int collectedCoins = 0;
    private int points = 0;

    public void CollectedCoin()
    {
        collectedCoins++;
        points += 20;
        Debug.Log("Collected coins: " + collectedCoins);
        OnPickedUpCoin?.Invoke(collectedCoins);
        UpdateScore?.Invoke(points);
        OnCollectedCoinSound?.Invoke();
    }

    public void DeadEnemy()
    {
        Debug.Log("�El enemigo me ha dicho que ha muerto!");
        points += 50;
        UpdateScore?.Invoke(points);
    }




    public void ResetManager()
    {
        collectedCoins = 0;
        points = 0;
    }
}
