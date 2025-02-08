using TMPro;
using UnityEngine;

public class ScoresController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManagerSO;
    [SerializeField] private HealthManager healthManagerSO;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinsText;

    private void Start()
    {
        scoreManagerSO.ResetManager();
    }
    private void OnEnable()
    {
        scoreManagerSO.UpdateScore += UpdateScoreText;
        scoreManagerSO.OnPickedUpCoin += UpdateCoinsText;
    }

    private void OnDisable()
    {
        scoreManagerSO.UpdateScore -= UpdateScoreText;
        scoreManagerSO.OnPickedUpCoin -= UpdateCoinsText;
    }

    private void UpdateScoreText(int points)
    {
        scoreText.text = "Score: " + points;
    }

    private void UpdateCoinsText (int collectedCoins)
    {
        coinsText.text = "Coins: " + collectedCoins;
    }

    private void WriteScore(int points)
    {
         
    }
}
