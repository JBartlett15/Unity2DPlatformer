using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int score;
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        score = 0;
        scoreText.text = "SCORE " + score.ToString();
    }

    public void ScoreChange(int amount)
    {
        score += amount;
        scoreText.text = "SCORE " + score.ToString();
    }
}
