using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public void LoadMenu()
    {
        FindObjectOfType<LevelManager>().LoadLevel("Menu");
    }
}
