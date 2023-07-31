using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int currency = 1000;

    private float currencyTimer = 5f;
    private Dictionary<int, float> currencyMap = new Dictionary<int, float>();

    private void Update()
    {
        currencyTimer -= Time.deltaTime;
        if (currencyTimer <= 0)
        {
            currencyTimer = 5f;
            AddCurrency(100);
        }

        foreach (Transform child in transform)
        {
            if (child.GetComponent<TMP_Text>() is TMP_Text txt)
            {
                txt.text = $"{currency} SHARDS";
            }
        }
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
    }

    public bool HasEnoughCurrency(int key)
    {
        float priceF = currencyMap.GetValueOrDefault(key, 100f);
        int price = (int)Mathf.Round(priceF);

        return price <= currency;
    }

    public bool BuyWithCurrency(int key)
    {
        float priceF = currencyMap.GetValueOrDefault(key, 100f);
        int price = (int)Mathf.Round(priceF);

        if (price <= currency)
        {
            currency -= price;
            float newPrice = priceF * 1.1f;
            if (currencyMap.ContainsKey(key))
            {
                currencyMap[key] = newPrice;
            }
            else
            {
                currencyMap.Add(key, newPrice);
            }
            return true;
        }
        return false;
    }
}
