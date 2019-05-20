public class Upgrade
{
    public UpgradesEnum UpgradeEnum;
    public int PossibleUpgradeOriginalAmount;

    public int TimesOfNewActiveIncomeCost;
    public int ActiveIncomeUpgradeFactor;

    public int FirstUpgradeCost;
    public int FurtherUpgradesAdditionalCost;
    public float AmountOfMoneyAddedEachPassiveIncomeUpgrade;

    public int AmountOfTimeForFirstNineUpgrades;
    public int AmountOfOneSecondUpgrades;
    public float AmountOfTimeForOtherUpgrades;

    public Upgrade(UpgradesEnum upgradeEnum, int possibleUpgradeOriginalAmount)
    {
        UpgradeEnum = upgradeEnum;
        PossibleUpgradeOriginalAmount = possibleUpgradeOriginalAmount;
    }

    public Upgrade(UpgradesEnum upgradeEnum, int possibleUpgradeOriginalAmount, int activeIncomeUpgradeFactor, int timesOfNewIncomeCost)
        : this(upgradeEnum, possibleUpgradeOriginalAmount)
    {
        ActiveIncomeUpgradeFactor = activeIncomeUpgradeFactor;
        TimesOfNewActiveIncomeCost = timesOfNewIncomeCost;
    }

    public Upgrade(UpgradesEnum upgradeEnum, int possibleUpgradeOriginalAmount, int firstUpgradeCost, int furtherUpgradesAdditionalCost, float amountOfMoneyAddedEachPassiveIncomeUpgrade)
        : this(upgradeEnum, possibleUpgradeOriginalAmount)
    {
        FirstUpgradeCost = firstUpgradeCost;
        FurtherUpgradesAdditionalCost = furtherUpgradesAdditionalCost;
        AmountOfMoneyAddedEachPassiveIncomeUpgrade = amountOfMoneyAddedEachPassiveIncomeUpgrade;
    }

    public Upgrade(UpgradesEnum upgradeEnum, int possibleUpgradeOriginalAmount, int firstUpgradeCost, int furtherUpgradesAdditionalCost, int amountOfTimeForFirstNineUpgrades, float amountOfTimeForOtherUpgrades, int amountOfOneSecondUpgrades)
        : this(upgradeEnum, possibleUpgradeOriginalAmount)
    {
        FirstUpgradeCost = firstUpgradeCost;
        FurtherUpgradesAdditionalCost = furtherUpgradesAdditionalCost;
        AmountOfTimeForFirstNineUpgrades = amountOfTimeForFirstNineUpgrades;
        AmountOfTimeForOtherUpgrades = amountOfTimeForOtherUpgrades;
        AmountOfOneSecondUpgrades = amountOfOneSecondUpgrades;
    }
}