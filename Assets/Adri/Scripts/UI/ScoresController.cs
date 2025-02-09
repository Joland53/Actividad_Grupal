using TMPro;
using UnityEngine;

public class ScoresController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManagerSO;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI finalScoreTextGO;
    [SerializeField] private TextMeshProUGUI finalScoreTextV;

    private void Start()
    {
        scoreManagerSO.ResetManager();
    }
    private void OnEnable()
    {
        scoreManagerSO.UpdateScore += UpdateScoreText;
        scoreManagerSO.UpdateScore += WriteFinalScore;
        scoreManagerSO.OnPickedUpCoin += UpdateCoinsText;
    }

    private void OnDisable()
    {
        scoreManagerSO.UpdateScore -= UpdateScoreText;
        scoreManagerSO.OnPickedUpCoin -= UpdateCoinsText;
        scoreManagerSO.UpdateScore -= WriteFinalScore;
    }

    private void UpdateScoreText(int points)
    {
        scoreText.text = "Score: " + points;
    }

    private void UpdateCoinsText (int collectedCoins)
    {
        coinsText.text = "Coins: " + collectedCoins + " / 68";
    }

    private void WriteFinalScore(int points)
    {
        finalScoreTextGO.text = "Your final score is: " + points;
        finalScoreTextV.text = "Your final score is: " + points;
    }
}
