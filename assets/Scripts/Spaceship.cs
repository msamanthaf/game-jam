using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spaceship : Building
{
    public static Spaceship current;
    public float healInterval;
    public int healAmountPerInterval;
    public GameObject gameOverScreen;

    private float healTimer;

    protected override void Start()
    {
        base.Start();

        if (current != null)
            Destroy(current);
        current = this;
    }

    protected override void Update()
    {
        if (curHP <= 0)
        {
            Score score = FindObjectOfType<Score>();
            score.IsGameOver = true;
            Destroy(gameObject);
            gameOverScreen.SetActive(true);
            TMP_Text txt = gameOverScreen.transform.GetChild(0).Find("Score")?.GetComponent<TMP_Text>();
            if (txt != null)
            {
                txt.text = score.ScoreHiScoreText;
            }
        }
        else
        {
            healTimer -= Time.deltaTime;
            if (healTimer <= 0)
            {
                healTimer = healInterval;
                curHP += healAmountPerInterval;
                curHP = Mathf.Min(curHP, maxHP);
            }
            base.Update();
        }
    }

    private void OnDestroy()
    {
        current = null;
    }
}
