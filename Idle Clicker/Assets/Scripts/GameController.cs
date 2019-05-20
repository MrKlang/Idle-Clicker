using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public const int TargetMoneySum = 3000000;
    public float CurrentMoney = 0;

    private float ActiveIncome = 1;
    private float PassiveIncome = 0;
    private float PassiveIncomeInterval = 10;
    private float FinalPlayTime;

    private UpgradesController UpgradesController;
    private WinPopupController WinPopupController;
    private BonusController BonusController;
    private TextsController TextsController;

    void Start()
    {
        UpgradesController.InitializeUpdatesCount();

        UpgradesController.SetNextActiveIncomeUpgradeCost(ActiveIncome);
        UpgradesController.SetNextPassiveIncomeUpgradeCost();
        UpgradesController.SetNextPassiveIncomeIntervalUpgradeCost();

        StartPassiveIncomeCoroutine();
    }

    private ref float GetActiveIncome() => ref ActiveIncome;

    private ref float GetPassiveIncome() => ref PassiveIncome;

    private ref float GetPassiveIncomeInterval() => ref PassiveIncomeInterval;

    private bool CanAffordActiveIncomeUpgrade() => CurrentMoney >= UpgradesController.NextActiveIncomeUpgradeCost;

    private bool CanAffordPassiveIncomeUpgrade() => CurrentMoney >= UpgradesController.NextPassiveIncomeUpgradeCost;

    private bool CanAffordPassiveIncomeIntervalUpgrade() => CurrentMoney >= UpgradesController.NextPassiveIncomeIntervalUpgradeCost;

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
        yield return new WaitForSeconds(GetPassiveIncomeInterval());

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

    public void IncreaseActiveIncome()
    {
        if (CanAffordActiveIncomeUpgrade() && UpgradesController.IsActiveIncomeUpgradeAvailable())
        {
            UpgradesController.IncreaseActiveIncome(ref GetActiveIncome());
            DeductUpdateFee(UpgradesController.NextActiveIncomeUpgradeCost);
            UpgradesController.SetNextActiveIncomeUpgradeCost(GetActiveIncome());
            TextsController.UpdateActiveIncomeText(ref GetActiveIncome());
        }
    }

    public void IncreasePassiveIncome()
    {
        if (CanAffordPassiveIncomeUpgrade() && UpgradesController.IsPassiveIncomeUpgradeAvailable())
        {
            UpgradesController.IncreasePassiveIncome(ref GetPassiveIncome());
            DeductUpdateFee(UpgradesController.NextPassiveIncomeUpgradeCost);
            UpgradesController.SetNextPassiveIncomeUpgradeCost();
            TextsController.UpdatePassiveIncomeText(ref GetPassiveIncome());
        }
    }

    public void DecreasePassiveIncomeInterval()
    {
        if (CanAffordPassiveIncomeIntervalUpgrade() && UpgradesController.IsPassiveIncomeIntervalUpgradeAvailable())
        {
            UpgradesController.DecreasePassiveIncomeInterval(ref GetPassiveIncomeInterval());
            DeductUpdateFee(UpgradesController.NextPassiveIncomeIntervalUpgradeCost);
            UpgradesController.SetNextPassiveIncomeIntervalUpgradeCost();
            TextsController.UpdatePassiveIncomeIntervalText(ref GetPassiveIncomeInterval());
        }
    }

    public void OnButtonClickedAction()
    {
        AddMoney(GetActiveIncome());
        TextsController.UpdateMoneyText(CurrentMoney, TargetMoneySum);
    }

    public void UpdateTotalMoneyText()
    {
        TextsController.UpdateMoneyText(CurrentMoney, TargetMoneySum);
    }
}
