using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField]
    private Button MainButton;
    [SerializeField]
    private Button MoreActiveIncomeButton;
    [SerializeField]
    private Button MorePassiveIncomeButton;
    [SerializeField]
    private Button FasterPassiveIncomeButton;

    public GameController GameController;

    void Start(){
        MainButton.onClick.AddListener(()=> 
        {
            GameController.AddMoney(GameController.GetActiveIncome());
            GameController.UpdateMoneyText();
        });

        MoreActiveIncomeButton.onClick.AddListener(()=>
        {
            UpdateActiveIncome();
            GameController.UpdateMoneyText();
        });

        MorePassiveIncomeButton.onClick.AddListener(() =>
        {
            UpdatePassiveIncome();
            GameController.UpdateMoneyText();
        });

        FasterPassiveIncomeButton.onClick.AddListener(()=> 
        {
            UpdatePassiveIncomeInterval();
            GameController.UpdateMoneyText();
        });
    }

    private void UpdateActiveIncome()
    {
        if(GameController.CanAffordActiveIncomeUpgrade() && GameController.UpgradesController.IsActiveIncomeUpgradeAvailable())
        {
            GameController.UpgradesController.IncreaseActiveIncome(ref GameController.GetActiveIncome());
            GameController.DeductUpdateFee(GameController.UpgradesController.NextActiveIncomeUpgradeCost);
            GameController.UpgradesController.SetNextActiveIncomeUpgradeCost(GameController.GetActiveIncome());
            GameController.UpdateActiveIncomeText();
        }
    }

    private void UpdatePassiveIncome()
    {
        if (GameController.CanAffordPassiveIncomeUpgrade() && GameController.UpgradesController.IsPassiveIncomeUpgradeAvailable())
        {
            GameController.UpgradesController.IncreasePassiveIncome(ref GameController.GetPassiveIncome());
            GameController.DeductUpdateFee(GameController.UpgradesController.NextPassiveIncomeUpgradeCost);
            GameController.UpgradesController.SetNextPassiveIncomeUpgradeCost();
            GameController.UpdatePassiveIncomeText();
        }
    }

    private void UpdatePassiveIncomeInterval()
    {
        if (GameController.CanAffordPassiveIncomeIntervalUpgrade() && GameController.UpgradesController.IsPassiveIncomeIntervalUpgradeAvailable())
        {
            GameController.UpgradesController.DecreasePassiveIncomeInterval(ref GameController.GetPassiveIncomeInterval());
            GameController.DeductUpdateFee(GameController.UpgradesController.NextPassiveIncomeIntervalUpgradeCost);
            GameController.UpgradesController.SetNextPassiveIncomeIntervalUpgradeCost();
            GameController.UpdatePassiveIncomeIntervalText();
        }
    }
}
