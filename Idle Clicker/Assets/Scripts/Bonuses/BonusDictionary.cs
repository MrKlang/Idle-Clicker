using System.Collections.Generic;
using System.Linq;


public enum BonusEnum
{
    HigherActiveIncome = 0,
    HigherPassiveIncome = 1,
    FasterPassiveIncome = 2
}

public class BonusDictionary
{
    public Dictionary<int, Bonus> Bonuses = new Dictionary<int, Bonus>() {
        { 1, new Bonus(BonusEnum.HigherActiveIncome,0.20f,60, 0.0f, "20% more active income (per each click).\nLasts one minute") },
        { 2, new Bonus(BonusEnum.HigherPassiveIncome,0.20f,60, 0.0f, "20% more passive income (over time).\nLasts one minute") },
        { 3, new Bonus(BonusEnum.FasterPassiveIncome,0.20f,60, 0.0f, "20% faster generation of passive income.\nLasts one minute") }
    };

    public Bonus GetBonusByKey(int key)
    {
        return Bonuses.Where(e => e.Key == key).First().Value;
    }
}
