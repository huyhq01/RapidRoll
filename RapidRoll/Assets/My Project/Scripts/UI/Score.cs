using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = UnityEngine.UI.Text;

public class Score : Singleton<Score>
{
    bool isScoreIncrease;
    int score;
    [SerializeField] Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = "Score : " + score;
    }

    private void Update()
    {
        if (isScoreIncrease) {
            score++;
            scoreText.text = "Score : " + score;
        }
    }

    public void IncreaseScore(bool yesnt)
    {
        isScoreIncrease = yesnt;
    }
}
