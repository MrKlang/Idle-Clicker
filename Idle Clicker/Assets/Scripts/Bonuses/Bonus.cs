using UnityEngine;

public class Bonus
{
    public BonusEnum BonusEnum;
    public float PercentageIncrease;
    public int DurationInSeconds;
    public float BonusCost;
    public string Description;

    public Bonus(BonusEnum bonusEnum, float percentageIncrease, int durationInSeconds, float bonusCost, string description)
    {
        BonusEnum = bonusEnum;
        PercentageIncrease = percentageIncrease;
        DurationInSeconds = durationInSeconds;
        BonusCost = bonusCost;
        Description = description;
    }
}
