using UnityEngine;
using UnityEditor;
using TMPro;

public class TextsController
{
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI ActiveIncomeMoneyText;
    public TextMeshProUGUI PassiveIncomeMoneyText;
    public TextMeshProUGUI PassiveIncomeIntervalText;

    public void UpdateMoneyText(float currentMoney, int targetMoneySum)
    {
        MoneyText.text = string.Format("$ {0}", currentMoney < targetMoneySum ? currentMoney.ToString("0.0") : targetMoneySum.ToString("0.0"));
    }

    public void UpdateActiveIncomeText(ref float activeIncome)
    {
        ActiveIncomeMoneyText.text = string.Format("$ per click: {0}", activeIncome);
    }

    public void UpdatePassiveIncomeText(ref float passiveIncome)
    {
        PassiveIncomeMoneyText.text = string.Format("$ over time: {0}", passiveIncome.ToString("0.0"));
    }

    public void UpdatePassiveIncomeIntervalText(ref float passiveIncomeInterval)
    {
        PassiveIncomeIntervalText.text = string.Format("Time interval: {0}s", passiveIncomeInterval.ToString("0.0"));
    }
}