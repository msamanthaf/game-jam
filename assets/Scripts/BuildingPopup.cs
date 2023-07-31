using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPopup : MonoBehaviour
{
    public int currentChoice = 0;
    public KeySet keys;

    private void Update()
    {
        if (Input.GetKeyDown(keys.LeftKey))
        {
            currentChoice--;
            if (currentChoice < 0)
            {
                currentChoice = 4;
            }
        }
        if (Input.GetKeyDown(keys.RightKey))
        {
            currentChoice++;
            if (currentChoice > 4)
            {
                currentChoice = 0;
            }
        }

        int i = 0;
        CurrencyManager cm = FindObjectOfType<CurrencyManager>();
        foreach (Transform choice in transform.Find("Choices"))
        {
            if (i >= 4 || cm.HasEnoughCurrency(i))
            {
                if (i == currentChoice)
                {
                    choice.GetComponent<SpriteRenderer>().color = Color.white;
                }
                else
                {
                    choice.GetComponent<SpriteRenderer>().color = Color.gray;
                }
            }
            else
            {
                if (i == currentChoice)
                {
                    choice.GetComponent<SpriteRenderer>().color = Color.magenta;
                }
                else
                {
                    choice.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            i++;
        }
    }
}
