using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public float fadeTime = 1.5f;

    private void Start()
    {
        FindObjectOfType<LevelManager>(true).fadeTimer = LevelManager.FADER_TIMER_MAX;
    }

    void Update()
    {
        if (Time.frameCount < 3) return;

        fadeTime -= Time.deltaTime;
        if (fadeTime <= 0)
        {
            FindObjectOfType<LevelManager>(true).LoadLevel("Menu");
            Destroy(gameObject);
        }
    }
}
