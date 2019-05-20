using UnityEngine;

public class Bonus : MonoBehaviour
{
    public BonusEnum BonusEnum;
    public float PercentageIncrease;
    public int DurationInSeconds;
    public float BonusCost;

    public Bonus(BonusEnum bonusEnum, float percentageIncrease, int durationInSeconds, float bonusCost)
    {
        BonusEnum = bonusEnum;
        PercentageIncrease = percentageIncrease;
        DurationInSeconds = durationInSeconds;
        BonusCost = bonusCost;
    }
}
