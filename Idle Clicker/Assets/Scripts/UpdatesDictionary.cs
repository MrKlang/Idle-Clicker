using System.Collections.Generic;
using System.Linq;

public enum UpgradesEnum
{
    ActiveIncomeIncrease = 0,
    PassiveIncomeIncrease = 1,
    PassiveIncomeIntervalDecrease = 2
}

public class UpgradesDictionary
{
    public Dictionary<int, Upgrade> Upgrades = new Dictionary<int, Upgrade>() {
        { 1, new Upgrade (UpgradesEnum.ActiveIncomeIncrease, 10, 2, 8) },
        { 2, new Upgrade (UpgradesEnum.PassiveIncomeIncrease, 26, 100, 200, 35.5f) },
        { 3, new Upgrade (UpgradesEnum.PassiveIncomeIntervalDecrease, 18, 100, 500, 1, 0.1f, 9) }
    };

    public Upgrade GetUpgradeByKey(int key)
    {
        return Upgrades.Where(e => e.Key == key).First().Value;
    }
}