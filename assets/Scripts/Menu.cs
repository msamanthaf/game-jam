using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void LoadGame()
    {
        FindObjectOfType<LevelManager>().LoadLevel("Instructions");
    }

    public void LoadAboutUs()
    {
        FindObjectOfType<LevelManager>().LoadLevel("About");
    }
}
