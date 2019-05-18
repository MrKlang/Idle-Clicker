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
            if (!GameController.HasTheGameGoalHasBeenCompleted())
            {
                GameController.AddMoneyFromButton();
                UpdateMoneyText();
            }
        });

        MoreActiveIncomeButton.onClick.AddListener(()=>
        {
            UpdateActiveIncome();
            UpdateMoneyText();
        });

        MorePassiveIncomeButton.onClick.AddListener(() =>
        {
            UpdatePassiveIncome();
            UpdateMoneyText();
        });

        FasterPassiveIncomeButton.onClick.AddListener(()=> 
        {
            UpdatePassiveIncomeInterval();
            UpdateMoneyText();
        });
    }

    public void UpdateMoneyText()
    {
        GameController.MoneyText.text = string.Format("$ {0}", GameController.CurrentMoney < GameController.TargetMoneySum ? GameController.CurrentMoney : GameController.TargetMoneySum);
    }

    private void UpdateActiveIncome()
    {
        if(GameController.CanAffordActiveIncomeUpgrade())
        {
            GameController.IncreaseActiveIncome();
            GameController.SetNextActiveIncomeUpgradeCost();
        }
    }

    private void UpdatePassiveIncome()
    {
        if (GameController.CanAffordPassiveIncomeUpgrade())
        {
            GameController.IncreasePassiveIncome();
            GameController.SetNextPassiveIncomeUpgradeCost();
        }
    }

    private void UpdatePassiveIncomeInterval()
    {
        if (GameController.CanAffordPassiveIncomeIntervalUpgrade())
        {
            GameController.DecreasePassiveIncomeInterval();
            GameController.SetNextPassiveIncomeIntervalUpgradeCost();
        }
    }

}
