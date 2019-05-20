using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public const int TargetMoneySum = 3000000;

    private float ActiveIncome = 1;
    private float PassiveIncome = 0;
    private float PassiveIncomeInterval = 10;
    private float FinalPlayTime;

    public float CurrentMoney = 0;

    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI ActiveIncomeMoneyText;
    public TextMeshProUGUI PassiveIncomeMoneyText;
    public TextMeshProUGUI PassiveIncomeIntervalText;

    public UpgradesController UpgradesController;
    public WinPopupController WinPopupController;

    void Start()
    {
        UpgradesController.InitializeUpdatesCount();

        UpgradesController.SetNextActiveIncomeUpgradeCost(ActiveIncome);
        UpgradesController.SetNextPassiveIncomeUpgradeCost();
        UpgradesController.SetNextPassiveIncomeIntervalUpgradeCost();

        StartPassiveIncomeCoroutine();
    }

    public ref float GetActiveIncome() => ref ActiveIncome;

    public ref float GetPassiveIncome() => ref PassiveIncome;

    public ref float GetPassiveIncomeInterval() => ref PassiveIncomeInterval;

    public bool CanAffordActiveIncomeUpgrade() => CurrentMoney >= UpgradesController.NextActiveIncomeUpgradeCost;

    public bool CanAffordPassiveIncomeUpgrade() => CurrentMoney >= UpgradesController.NextPassiveIncomeUpgradeCost;

    public bool CanAffordPassiveIncomeIntervalUpgrade() => CurrentMoney >= UpgradesController.NextPassiveIncomeIntervalUpgradeCost;

    public void DeductUpdateFee(float fee) => CurrentMoney -= fee;

    public bool HasTheGameGoalHasBeenCompleted()
    {
        if (CurrentMoney >= TargetMoneySum)
        {
            return true;
        }

        return false;
    }

    public void AddMoney(float income)
    {
        CurrentMoney += income;

        if (HasTheGameGoalHasBeenCompleted())
        {
            SaveBestTime();
            WinPopupController.ShowWinPopup(FinalPlayTime);
        }
    }

    public void StartPassiveIncomeCoroutine()
    {
        StartCoroutine(PassiveIncomeCoroutine());
    }

    public void UpdateMoneyText()
    {
        MoneyText.text = string.Format("$ {0}", CurrentMoney < TargetMoneySum ? CurrentMoney.ToString("0.0") : TargetMoneySum.ToString("0.0"));
    }

    public void UpdateActiveIncomeText()
    {
        ActiveIncomeMoneyText.text = string.Format("$ per click: {0}", ActiveIncome);
    }

    public void UpdatePassiveIncomeText()
    {
        PassiveIncomeMoneyText.text = string.Format("$ over time: {0}", PassiveIncome.ToString("0.0"));
    }

    public void UpdatePassiveIncomeIntervalText()
    {
        PassiveIncomeIntervalText.text = string.Format("Time interval: {0}s", PassiveIncomeInterval.ToString("0.0"));
    }

    private IEnumerator PassiveIncomeCoroutine()
    {
        yield return new WaitForSeconds(GetPassiveIncomeInterval());

        if (PassiveIncome > 0)
        {
            AddMoney(PassiveIncome);
            UpdateMoneyText();
        }

        StartPassiveIncomeCoroutine();
    }

    private float GetPlayTime()
    {
        return Time.timeSinceLevelLoad;
    }

    private void SaveBestTime()
    {
        FinalPlayTime = GetPlayTime();

        if (PlayerPrefs.HasKey("BestTime"))
        {
            if(FinalPlayTime < PlayerPrefs.GetFloat("BestTime"))
            {
                PlayerPrefs.SetFloat("BestTime", GetPlayTime());
            }
        }
        else
        {
            PlayerPrefs.SetFloat("BestTime", GetPlayTime());
        }
    }
}
