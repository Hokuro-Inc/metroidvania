using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private FloatValue currency;
    [SerializeField] private Text currencyText;

    void Start()
    {
        currencyText.text = currency.initialValue.ToString();
    }

    public void UpdateCurrencyDisplay()
    {
        currencyText.text = currency.initialValue.ToString();
    }
}
