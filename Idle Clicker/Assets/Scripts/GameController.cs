using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public const int TargetMoneySum = 3000000;
    public float CurrentMoney = 0;

    private float ActiveIncome = 1;
    private float PassiveIncome = 0;
    private float PassiveIncomeInterval = 10;
    private float FinalPlayTime;

    private float PreBonusActiveIncome;
    private float PreBonusPassiveIncome;
    private float PreBonusPassiveIncomeInterval;

    private UpgradesController UpgradesController;
    private BonusDictionary BonusDictionary;
    public TextsController TextsController;
    public WinPopupController WinPopupController;

    public List<Bonus> CurrentlyUsedBonuses = new List<Bonus>();

    void Start()
    {
        UpgradesController = new UpgradesController();
        BonusDictionary = new BonusDictionary();
        PreBonusActiveIncome = ActiveIncome;
        PreBonusPassiveIncome = PassiveIncome;
        PreBonusPassiveIncomeInterval = PassiveIncomeInterval;

        UpgradesController.InitializeUpdatesCount();
        UpgradesController.SetNextActiveIncomeUpgradeCost(ActiveIncome);
        UpgradesController.SetNextPassiveIncomeUpgradeCost();
        UpgradesController.SetNextPassiveIncomeIntervalUpgradeCost();

        UpdateAllTexts();

        StartPassiveIncomeCoroutine();
    }

    private ref float GetActiveIncomeRef() => ref ActiveIncome;

    private ref float GetPassiveIncomeRef() => ref PassiveIncome;

    private ref float GetPassiveIncomeIntervalRef() => ref PassiveIncomeInterval;

    private ref float GetPreBonusActiveIncomeRef() => ref PreBonusActiveIncome;

    private ref float GetPreBonusPassiveIncomeRef() => ref PreBonusPassiveIncome;

    private ref float GetPreBonusPassiveIncomeIntervalRef() => ref PreBonusPassiveIncomeInterval;

    private bool CanAffordActiveIncomeUpgrade() => CurrentMoney >= UpgradesController.NextActiveIncomeUpgradeCost;

    private bool CanAffordPassiveIncomeUpgrade() => CurrentMoney >= UpgradesController.NextPassiveIncomeUpgradeCost;

    private bool CanAffordPassiveIncomeIntervalUpgrade() => CurrentMoney >= UpgradesController.NextPassiveIncomeIntervalUpgradeCost;

    public bool IsActiveIncomeUpgradeAvailable() => UpgradesController.CurrentPossibleActiveIncomeUpdateCount > 0;

    public bool IsPassiveIncomeUpgradeAvailable() => UpgradesController.CurrentPossiblePassiveIncomeUpdateCount > 0;

    public bool IsPassiveIncomeIntervalUpgradeAvailable() => UpgradesController.CurrentPossiblePassiveIncomeIntervalUpdateCount > 0;

    private void DeductUpdateFee(float fee) => CurrentMoney -= fee;

    private bool HasTheGameGoalBeenCompleted()
    {
        if (CurrentMoney >= TargetMoneySum)
        {
            return true;
        }

        return false;
    }

    private void AddMoney(float income)
    {
        CurrentMoney += income;

        if (HasTheGameGoalBeenCompleted())
        {
            SaveBestTime();
            WinPopupController.ShowWinPopup(FinalPlayTime);
        }
    }

    private void StartPassiveIncomeCoroutine()
    {
        StartCoroutine(PassiveIncomeCoroutine());
    }

    private IEnumerator PassiveIncomeCoroutine()
    {
        yield return new WaitForSeconds(GetPassiveIncomeIntervalRef());

        if (PassiveIncome > 0)
        {
            AddMoney(PassiveIncome);
            TextsController.UpdateMoneyText(CurrentMoney,TargetMoneySum);
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

    public float DefineNextActiveIncomeUpgradeCost(ref float currentIncome, ref float incomeBeforeBonus)
    {
        return incomeBeforeBonus != currentIncome ? currentIncome - (currentIncome - incomeBeforeBonus) : currentIncome;
    }

    public void IncreaseActiveIncome()
    {
        if (CanAffordActiveIncomeUpgrade() && IsActiveIncomeUpgradeAvailable())
        {
            UpgradesController.IncreaseActiveIncome(ref GetActiveIncomeRef(), ref GetPreBonusActiveIncomeRef());
            DeductUpdateFee(UpgradesController.NextActiveIncomeUpgradeCost);
            UpgradesController.SetNextActiveIncomeUpgradeCost(DefineNextActiveIncomeUpgradeCost(ref GetActiveIncomeRef(), ref GetPreBonusActiveIncomeRef()));
        }
    }

    public void IncreasePassiveIncome()
    {
        if (CanAffordPassiveIncomeUpgrade() && IsPassiveIncomeUpgradeAvailable())
        {
            UpgradesController.IncreasePassiveIncome(ref GetPassiveIncomeRef(), ref GetPreBonusPassiveIncomeRef());
            OverwriteValuesWithBonus(BonusDictionary.GetBonusByKey(2), ref GetPassiveIncomeRef(), ref GetPreBonusPassiveIncomeRef());
            DeductUpdateFee(UpgradesController.NextPassiveIncomeUpgradeCost);
            UpgradesController.SetNextPassiveIncomeUpgradeCost();
        }
    }

    public void DecreasePassiveIncomeInterval()
    {
        if (CanAffordPassiveIncomeIntervalUpgrade() && IsPassiveIncomeIntervalUpgradeAvailable())
        {
            UpgradesController.DecreasePassiveIncomeInterval(ref GetPassiveIncomeIntervalRef(), ref GetPreBonusPassiveIncomeIntervalRef());
            OverwriteValuesWithBonus(BonusDictionary.GetBonusByKey(3), ref GetPassiveIncomeIntervalRef(), ref GetPreBonusPassiveIncomeIntervalRef());
            DeductUpdateFee(UpgradesController.NextPassiveIncomeIntervalUpgradeCost);
            UpgradesController.SetNextPassiveIncomeIntervalUpgradeCost();
        }
    }

    public void OnButtonClickedAction()
    {
        AddMoney(GetActiveIncomeRef());
        TextsController.UpdateMoneyText(CurrentMoney, TargetMoneySum);
    }

    public bool CanBonusBeUsed(Bonus bonus)
    {
        return !CurrentlyUsedBonuses.Contains(bonus);
    }

    public void StartActiveIncomeBonusCoroutine(Bonus bonus)
    {
        if (CanBonusBeUsed(bonus))
        {
            CurrentlyUsedBonuses.Add(bonus);

            switch (bonus.BonusEnum)
            {
                case BonusEnum.HigherActiveIncome:
                    StartCoroutine(BonusCoroutine(bonus, ActiveIncome, PreBonusActiveIncome, ((newIncome, oldIncome) => { ActiveIncome = newIncome; PreBonusActiveIncome = oldIncome; })));
                    break;
                case BonusEnum.HigherPassiveIncome:
                    StartCoroutine(BonusCoroutine(bonus, PassiveIncome, PreBonusPassiveIncome, ((newIncome, oldIncome) => { PassiveIncome = newIncome; PreBonusPassiveIncome = oldIncome; })));
                    break;
                case BonusEnum.FasterPassiveIncome:
                    StartCoroutine(BonusCoroutine(bonus, PassiveIncomeInterval, PreBonusPassiveIncomeInterval, ((newIncome, oldIncome) => { PassiveIncomeInterval = newIncome; PreBonusPassiveIncomeInterval = oldIncome; })));
                    break;
            }
        }
    }

    public void UpdateAllTexts()
    {
        TextsController.UpdateMoneyText(CurrentMoney, TargetMoneySum);
        TextsController.UpdateActiveIncomeText(ActiveIncome, ActiveIncome - PreBonusActiveIncome);
        TextsController.UpdatePassiveIncomeText(PassiveIncome, PassiveIncome - PreBonusPassiveIncome );
        TextsController.UpdatePassiveIncomeIntervalText(PassiveIncomeInterval,  PassiveIncomeInterval - PreBonusPassiveIncomeInterval);

        TextsController.UpdateMoreActiveIncomeButtonText(UpgradesController.NextActiveIncomeUpgradeCost);
        TextsController.UpdateMorePassiveIncomeButtonText(UpgradesController.NextPassiveIncomeUpgradeCost);
        TextsController.UpdateMorePassiveIncomeIntervalButtonText(UpgradesController.NextPassiveIncomeIntervalUpgradeCost);
    }

    private void OverwriteValuesWithBonus(Bonus bonus, ref float incomeOrInterval, ref float incomeOrIntervalPreBonus)
    {
        if (incomeOrInterval != incomeOrIntervalPreBonus)
        {
            if (bonus.BonusEnum != BonusEnum.FasterPassiveIncome)
            {
                incomeOrInterval = incomeOrIntervalPreBonus + (incomeOrIntervalPreBonus * bonus.PercentageIncrease);
            }
            else
            {
                incomeOrInterval = incomeOrIntervalPreBonus - (incomeOrIntervalPreBonus * bonus.PercentageIncrease);
            }
        }
    }

    private IEnumerator BonusCoroutine(Bonus bonus, float incomeOrInterval, float incomeOrIntervalPreBonus, Action<float,float> overwritingAction)
    {
        incomeOrIntervalPreBonus = incomeOrInterval;
        if (bonus.BonusEnum != BonusEnum.FasterPassiveIncome)
        {
            incomeOrInterval += incomeOrInterval * bonus.PercentageIncrease;
        }
        else
        {
            incomeOrInterval -= incomeOrInterval * bonus.PercentageIncrease;
        }
        overwritingAction(incomeOrInterval, incomeOrIntervalPreBonus);
        UpdateAllTexts();

        yield return new WaitForSeconds(bonus.DurationInSeconds);
            
        EndBonus(bonus);
        UpdateAllTexts();
    }

    private void EndBonus(Bonus bonus)
    {
        CurrentlyUsedBonuses.Remove(bonus);
        switch (bonus.BonusEnum)
        {
            case BonusEnum.HigherActiveIncome :
                ActiveIncome = PreBonusActiveIncome;
                break;
            case BonusEnum.HigherPassiveIncome :
                PassiveIncome = PreBonusPassiveIncome;
                break;
            case BonusEnum.FasterPassiveIncome :
                PassiveIncomeInterval = PreBonusPassiveIncomeInterval;
                break;
        }
    }

    public void ModifyMoneyAmountOnEventCompletion(int value, bool isAbsolute)
    {
        if (isAbsolute)
        {
            CurrentMoney += value;
        }
        else
        {
            CurrentMoney += CurrentMoney * ((float)value/100);
        }

        UpdateAllTexts();
    }
}
