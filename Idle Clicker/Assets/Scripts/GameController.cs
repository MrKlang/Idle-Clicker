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

    private int CurrentPossibleActiveIncomeUpdateCount;
    private int CurrentPossiblePassiveIncomeUpdateCount;
    private int CurrentPossiblePassiveIncomeIntervalUpdateCount;

    public float CurrentMoney = 0;
    public UpgradesDictionary UpgradesDictionary = new UpgradesDictionary();

    public int NextActiveIncomeUpgradeCost;
    public int NextPassiveIncomeUpgradeCost;
    public int NextPassiveIncomeIntervalUpgradeCost;

    public TextMeshProUGUI MoneyText;

    void Start()
    {
        CurrentPossibleActiveIncomeUpdateCount = UpgradesDictionary.GetUpgradeByKey(1).PossibleUpgradeOriginalAmount;
        CurrentPossiblePassiveIncomeUpdateCount = UpgradesDictionary.GetUpgradeByKey(2).PossibleUpgradeOriginalAmount;
        CurrentPossiblePassiveIncomeIntervalUpdateCount = UpgradesDictionary.GetUpgradeByKey(3).PossibleUpgradeOriginalAmount;

        SetNextActiveIncomeUpgradeCost();
        SetNextPassiveIncomeUpgradeCost();
        SetNextPassiveIncomeIntervalUpgradeCost();
    }

    public void AddMoneyFromButton() => CurrentMoney += ActiveIncome;

    public void AddMoneyOverTime() => CurrentMoney += PassiveIncome;

    public bool CanAffordActiveIncomeUpgrade() => CurrentMoney >= NextActiveIncomeUpgradeCost;

    public bool CanAffordPassiveIncomeUpgrade() => CurrentMoney >= NextPassiveIncomeUpgradeCost;

    public bool CanAffordPassiveIncomeIntervalUpgrade()
    {
        var passiveIncomeIntervalFromDictionary = UpgradesDictionary.GetUpgradeByKey(3);

        if (CurrentPossiblePassiveIncomeIntervalUpdateCount == passiveIncomeIntervalFromDictionary.PossibleUpgradeOriginalAmount)
        {
            return CurrentMoney >= passiveIncomeIntervalFromDictionary.FirstUpgradeCost;
        }

        return CurrentMoney >= passiveIncomeIntervalFromDictionary.FurtherUpgradesAdditionalCost;
    }

    public void SetNextActiveIncomeUpgradeCost()
    {
        if (CurrentPossibleActiveIncomeUpdateCount > 0)
        {
            var activeIncomeUpgradeFromDictionary = UpgradesDictionary.GetUpgradeByKey(1);
            NextActiveIncomeUpgradeCost = (int)(ActiveIncome*activeIncomeUpgradeFromDictionary.ActiveIncomeUpgradeFactor * activeIncomeUpgradeFromDictionary.TimesOfNewActiveIncomeCost);
        }
    }

    public void SetNextPassiveIncomeUpgradeCost()
    {
        if (CurrentPossiblePassiveIncomeUpdateCount == UpgradesDictionary.GetUpgradeByKey(2).PossibleUpgradeOriginalAmount)
        {
            NextPassiveIncomeUpgradeCost += UpgradesDictionary.GetUpgradeByKey(2).FirstUpgradeCost;
        }
        else
        {
            NextPassiveIncomeUpgradeCost += UpgradesDictionary.GetUpgradeByKey(2).FurtherUpgradesAdditionalCost;
        }
    }

    public void SetNextPassiveIncomeIntervalUpgradeCost()
    {
        if (CurrentPossiblePassiveIncomeIntervalUpdateCount == UpgradesDictionary.GetUpgradeByKey(3).PossibleUpgradeOriginalAmount)
        {
            NextPassiveIncomeIntervalUpgradeCost = UpgradesDictionary.GetUpgradeByKey(3).FirstUpgradeCost;
        }
        else
        {
            NextPassiveIncomeIntervalUpgradeCost = UpgradesDictionary.GetUpgradeByKey(3).FurtherUpgradesAdditionalCost;
        }
    }

    public void IncreaseActiveIncome()
    {
        if (CurrentPossibleActiveIncomeUpdateCount > 0)
        {
            ActiveIncome *= UpgradesDictionary.GetUpgradeByKey(1).ActiveIncomeUpgradeFactor;
            CurrentPossibleActiveIncomeUpdateCount--;
            DeductUpdateFee(NextActiveIncomeUpgradeCost);
        }
    }

    public void IncreasePassiveIncome()
    {
        if (CurrentPossiblePassiveIncomeUpdateCount > 0)
        {
            PassiveIncome += UpgradesDictionary.GetUpgradeByKey(2).AmountOfMoneyAddedEachPassiveIncomeUpgrade;
            CurrentPossiblePassiveIncomeUpdateCount--;
            DeductUpdateFee(NextPassiveIncomeUpgradeCost);
        }
    }

    public void DecreasePassiveIncomeInterval()
    {
        if (CurrentPossiblePassiveIncomeIntervalUpdateCount > 9)
        {
            PassiveIncomeInterval--;
            CurrentPossiblePassiveIncomeIntervalUpdateCount--;
            DeductUpdateFee(NextPassiveIncomeIntervalUpgradeCost);
        }
        else if(CurrentPossiblePassiveIncomeIntervalUpdateCount > 0)
        {
            PassiveIncomeInterval -= UpgradesDictionary.GetUpgradeByKey(3).AmountOfTimeForOtherUpgrades;
            CurrentPossiblePassiveIncomeIntervalUpdateCount--;
            DeductUpdateFee(NextPassiveIncomeIntervalUpgradeCost);
        }
    }

    public void DeductUpdateFee(float fee)
    {
        CurrentMoney -= fee;
    }

    public bool HasTheGameGoalHasBeenCompleted()
    {
        if (CurrentMoney >= TargetMoneySum)
        {
            return true;
        }

        return false;
    }
}
