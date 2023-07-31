using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class About : MonoBehaviour
{
    public void Close(){
        FindObjectOfType<LevelManager>(true).LoadLevel("Menu");
    }
}
