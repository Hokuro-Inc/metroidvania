using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Text currencyText;

    void Start()
    {
        currencyText.text = playerInventory.currency.ToString();
    }

    public void UpdateCurrencyDisplay()
    {
        currencyText.text = playerInventory.currency.ToString();
    }
}
