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
    [SerializeField]
    private Button BonusesButton;

    public GameController GameController;

    void Start(){
        MainButton.onClick.AddListener(()=> 
        {
            GameController.OnButtonClickedAction();
        });

        MoreActiveIncomeButton.onClick.AddListener(()=>
        {
            UpdateActiveIncome();
            GameController.UpdateTotalMoneyText();
        });

        MorePassiveIncomeButton.onClick.AddListener(() =>
        {
            UpdatePassiveIncome();
            GameController.UpdateTotalMoneyText();
        });

        FasterPassiveIncomeButton.onClick.AddListener(()=> 
        {
            UpdatePassiveIncomeInterval();
            GameController.UpdateTotalMoneyText();
        });
    }

    private void UpdateActiveIncome()
    {
        GameController.IncreaseActiveIncome();
    }

    private void UpdatePassiveIncome()
    {
        GameController.IncreasePassiveIncome();
    }

    private void UpdatePassiveIncomeInterval()
    {
        GameController.DecreasePassiveIncomeInterval();
    }
}
