using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : Player
{
    public BuildingPopup popup;
    public Building[] buildings;

    protected override void Start()
    {
        base.Start();
        popup.gameObject.SetActive(false);
    }

    public override void PerformAction()
    {
        if (Input.GetKeyDown(keys.ActionKey))
        {
            if (popup.gameObject.activeInHierarchy)
            {
                if (popup.currentChoice >= 0 && popup.currentChoice < 4)
                {
                    Build(popup.currentChoice);
                }
                popup.gameObject.SetActive(false);
            }
            else
            {
                popup.gameObject.SetActive(true);
                popup.transform.position = transform.position;
            }
        }
    }

    public override void PostUpdate()
    {
        if (FindObjectOfType<Maintainer>().movement.magnitude > 0.1f)
        {
            movement *= 0.5f;
        }
        if (popup.gameObject.activeInHierarchy)
        {
            movement = Vector2.zero;
        }
    }

    private void Build(int buildingIndex)
    {
        bool s = FindObjectOfType<CurrencyManager>().BuyWithCurrency(buildingIndex);
        if (s)
        {
            Instantiate(buildings[buildingIndex], transform.position, Quaternion.identity, GameObject.Find("Buildings").transform);
        }
    }
}
