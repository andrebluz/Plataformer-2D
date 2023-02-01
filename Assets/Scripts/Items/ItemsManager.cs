using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;

public class ItemsManager : Singleton<ItemsManager>
{

    public int coins;
    public TextMeshProUGUI uiCoins;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
        uiCoins.text = coins.ToString();
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        uiCoins.text = coins.ToString();
    }
}
