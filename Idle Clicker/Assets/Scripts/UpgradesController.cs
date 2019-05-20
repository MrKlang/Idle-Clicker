using UnityEngine;
using UnityEditor;

public class UpgradesController : MonoBehaviour
{ 
    public int CurrentPossibleActiveIncomeUpdateCount { get; private set; }
    public int CurrentPossiblePassiveIncomeUpdateCount { get; private set; }
    public int CurrentPossiblePassiveIncomeIntervalUpdateCount { get; private set; }

    public int NextActiveIncomeUpgradeCost { get; private set; }
    public int NextPassiveIncomeUpgradeCost { get; private set; }
    public int NextPassiveIncomeIntervalUpgradeCost { get; private set; }

    public UpgradesDictionary UpgradesDictionary = new UpgradesDictionary();

    public void InitializeUpdatesCount()
    {
        CurrentPossibleActiveIncomeUpdateCount = UpgradesDictionary.GetUpgradeByKey(1).PossibleUpgradeOriginalAmount;
        CurrentPossiblePassiveIncomeUpdateCount = UpgradesDictionary.GetUpgradeByKey(2).PossibleUpgradeOriginalAmount;
        CurrentPossiblePassiveIncomeIntervalUpdateCount = UpgradesDictionary.GetUpgradeByKey(3).PossibleUpgradeOriginalAmount;
    }

    public bool IsActiveIncomeUpgradeAvailable() => CurrentPossibleActiveIncomeUpdateCount > 0;

    public bool IsPassiveIncomeUpgradeAvailable() => CurrentPossiblePassiveIncomeUpdateCount > 0;

    public bool IsPassiveIncomeIntervalUpgradeAvailable() => CurrentPossiblePassiveIncomeIntervalUpdateCount > 0;

    public void SetNextActiveIncomeUpgradeCost(float income)
    {
        if (CurrentPossibleActiveIncomeUpdateCount > 0)
        {
            var activeIncomeUpgradeFromDictionary = UpgradesDictionary.GetUpgradeByKey(1);
            NextActiveIncomeUpgradeCost = (int)(income * activeIncomeUpgradeFromDictionary.ActiveIncomeUpgradeFactor * activeIncomeUpgradeFromDictionary.TimesOfNewActiveIncomeCost);
        }
    }

    public void SetNextPassiveIncomeUpgradeCost()
    {
        if (CurrentPossiblePassiveIncomeUpdateCount == UpgradesDictionary.GetUpgradeByKey(2).PossibleUpgradeOriginalAmount)
        {
            NextPassiveIncomeUpgradeCost += UpgradesDictionary.GetUpgradeByKey(2).FirstUpgradeCost;
        }
        else if(CurrentPossiblePassiveIncomeUpdateCount > 0)
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
        else if(CurrentPossiblePassiveIncomeUpdateCount > 0)
        {
            NextPassiveIncomeIntervalUpgradeCost = UpgradesDictionary.GetUpgradeByKey(3).FurtherUpgradesAdditionalCost;
        }
    }

    public void IncreaseActiveIncome(ref float income)
    {
        income *= UpgradesDictionary.GetUpgradeByKey(1).ActiveIncomeUpgradeFactor;
        CurrentPossibleActiveIncomeUpdateCount--;
    }

    public void IncreasePassiveIncome(ref float income)
    {
        income += UpgradesDictionary.GetUpgradeByKey(2).AmountOfMoneyAddedEachPassiveIncomeUpgrade;
        CurrentPossiblePassiveIncomeUpdateCount--;
    }

    public void DecreasePassiveIncomeInterval(ref float interval)
    {
        if (CurrentPossiblePassiveIncomeIntervalUpdateCount > 9)
        {
            interval--;
            CurrentPossiblePassiveIncomeIntervalUpdateCount--;
        }
        else
        {
            interval -= UpgradesDictionary.GetUpgradeByKey(3).AmountOfTimeForOtherUpgrades;
            CurrentPossiblePassiveIncomeIntervalUpdateCount--;
        }
    }
}