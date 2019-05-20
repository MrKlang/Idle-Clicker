using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum BonusEnum
{
    HigherActiveIncome = 0,
    HigherPassiveIncome = 1,
    FasterPassiveIncome = 2
}

public class BonusDictionary : MonoBehaviour
{
    public Dictionary<int, Bonus> Bonuses = new Dictionary<int, Bonus>() {
        { 1, new Bonus(BonusEnum.HigherActiveIncome,0.20f,60, 0.0f) },
        { 2, new Bonus(BonusEnum.HigherPassiveIncome,0.20f,60, 0.0f) },
        { 3, new Bonus(BonusEnum.FasterPassiveIncome,0.20f,60, 0.0f) }
    };

    public Bonus GetUpgradeByKey(int key)
    {
        return Bonuses.Where(e => e.Key == key).First().Value;
    }
}
