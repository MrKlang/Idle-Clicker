using UnityEngine;
using UnityEditor;
using TMPro;

public class TextsController : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI ActiveIncomeMoneyText;
    public TextMeshProUGUI PassiveIncomeMoneyText;
    public TextMeshProUGUI PassiveIncomeIntervalText;
    public TextMeshProUGUI MoreActiveIncomeButtonText;
    public TextMeshProUGUI MorePassiveIncomeButtonText;
    public TextMeshProUGUI MorePassiveIncomeIntervalButtonText;

    public void UpdateMoneyText(float currentMoney, int targetMoneySum)
    {
        MoneyText.text = string.Format("$ {0}", currentMoney < targetMoneySum ? currentMoney.ToString("0.0") : targetMoneySum.ToString("0.0"));
    }

    public void UpdateActiveIncomeText(float activeIncome, float bonusActiveIncome)
    {
        ActiveIncomeMoneyText.text = string.Format("$ per click: {0} with {1} bonus", activeIncome, bonusActiveIncome.ToString("0.0"));
    }

    public void UpdatePassiveIncomeText(float passiveIncome, float bonusPassiveIncome)
    {
        PassiveIncomeMoneyText.text = string.Format("$ over time: {0} with {1} bonus", passiveIncome.ToString("0.0"), bonusPassiveIncome.ToString("0.0"));
    }

    public void UpdatePassiveIncomeIntervalText(float passiveIncomeInterval, float bonusInterval)
    {
        PassiveIncomeIntervalText.text = string.Format("Time interval: {0}s with {1} bonus", passiveIncomeInterval.ToString("0.00"), bonusInterval.ToString("0.00"));
    }

    public void UpdateMoreActiveIncomeButtonText(int nextActiveIncomeUpdateCost)
    {
        MoreActiveIncomeButtonText.text = string.Format("Increase muny per click ({0}$)", nextActiveIncomeUpdateCost);
    }

    public void UpdateMorePassiveIncomeButtonText(int nextPassiveIncomeUpdateCost)
    {
        MorePassiveIncomeButtonText.text = string.Format("Increase muny over time ({0}$)", nextPassiveIncomeUpdateCost);
    }

    public void UpdateMorePassiveIncomeIntervalButtonText(int nextPassivIncomeIntervalUpdateCost)
    {
        MorePassiveIncomeIntervalButtonText.text = string.Format("Decrease muny over time interval ({0}$)", nextPassivIncomeIntervalUpdateCost);
    }
}