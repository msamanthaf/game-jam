using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public const float FADER_TIMER_MAX = 1f;//fade in and out in this many seconds 
    public static LevelManager current;

    public Image fader;

    /// <summary>
    /// -1000 means no fading.
    /// </summary>
    public float fadeTimer = -1000;

    private bool fadeIn = true;//true if fading in, false if fading out
    private string sceneToLoad = null;

    private void Start()
    {
        if (current != null)
        {
            Destroy(current);// if there is already a level manager instance, destroy the previous one and replace with this one.
        }
        DontDestroyOnLoad(transform.parent.gameObject);
        current = this;
    }

    /// <summary>
    /// Note that fade in will still occur.
    /// </summary>
    /// <param name="name"></param>
    public void LoadLevelNoFade(string name)
    {
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Fades out and loads the scene with the given name.
    /// </summary>
    /// <param name="name"></param>
    public void LoadLevel(string name)
    {
        sceneToLoad = name;
        fadeTimer = FADER_TIMER_MAX;
        fadeIn = false;
    }

    private void Update()
    {
        // no fading
        if (fadeTimer <= -1000 || Time.frameCount < 3) return;

        fadeTimer -= Time.deltaTime;
        if (fadeIn)
        {
            fader.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, fadeTimer / FADER_TIMER_MAX);
        }
        else
        {
            fader.color = Color.Lerp(Color.black, new Color(0, 0, 0, 0), fadeTimer / FADER_TIMER_MAX);
        }

        if (fadeTimer <= 0)
        {
            if (fadeIn)
            {
                sceneToLoad = null;
                fadeTimer = -1000;
                fader.color = new Color(0, 0, 0, 0);
            }
            else
            {
                fadeIn = true;
                fadeTimer = FADER_TIMER_MAX;
                LoadLevelNoFade(sceneToLoad);
                sceneToLoad = null;
            }
        }
    }
}
