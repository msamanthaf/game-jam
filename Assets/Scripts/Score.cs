using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score = 0;

    private float scoreTimer = 0f;
    private bool isGameOver = false;

    public bool IsGameOver
    {
        get => isGameOver;
        set
        {
            if (value)
                SetHiScore();

            isGameOver = value;
        }
    }

    private void Update()
    {
        if (isGameOver) return;
        scoreTimer += Time.deltaTime;
        score = Mathf.FloorToInt(scoreTimer / 10f);

        foreach (Transform child in transform)
        {
            if (child.GetComponent<TMP_Text>() is TMP_Text text)
            {
                text.text = $"SCORE: {score}";
            }
        }
    }

    public int GetHiScore()
    {
        return Mathf.Max(score, PlayerPrefs.GetInt("Score"));
    }

    public void SetHiScore()
    {
        PlayerPrefs.SetInt("Score", GetHiScore());
    }

    public string ScoreHiScoreText
    {
        get
        {
            int hi = GetHiScore();
            return $"SCORE: {score}\nHI SCORE: {hi}. " + (hi == score ? "NEW BEST!" : "");
        }
    }
}
