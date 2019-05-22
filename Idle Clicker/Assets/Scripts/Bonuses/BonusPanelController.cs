using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusPanelController : MonoBehaviour
{
    [SerializeField]
    private Button ActiveIncomeBonusBuyButton;
    [SerializeField]
    private Button PassiveIncomeBonusBuyButton;
    [SerializeField]
    private Button PassiveIncomeIntervalBonusBuyButton;
    [SerializeField]
    private Button CloseButton;

    [SerializeField]
    private TextMeshProUGUI ActiveIncomeBonusDescription;
    [SerializeField]
    private TextMeshProUGUI PassiveIncomeBonusDescription;
    [SerializeField]
    private TextMeshProUGUI PassiveIncomeIntervalBonusDescription;

    [SerializeField]
    private TextMeshProUGUI ActiveIncomeBonusPrice;
    [SerializeField]
    private TextMeshProUGUI PassiveIncomeBonusPrice;
    [SerializeField]
    private TextMeshProUGUI PassiveIncomeIntervalBonusPrice;

    private BonusDictionary BonusDictionary = new BonusDictionary();
    public GameController GameController;

    public void ShowPanel()
    {
        DefineInteractibleButtons();
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        GameController.UpdateAllTexts();
    }

    private void Start()
    {
        BonusDictionary = new BonusDictionary();

        CloseButton.onClick.AddListener(()=> 
        {
            HidePanel();
        });

        ActiveIncomeBonusBuyButton.onClick.AddListener(()=> 
        {
            GameController.StartActiveIncomeBonusCoroutine(BonusDictionary.GetBonusByKey(1));
            HidePanel();
        });

        PassiveIncomeBonusBuyButton.onClick.AddListener(()=> {
            GameController.StartActiveIncomeBonusCoroutine(BonusDictionary.GetBonusByKey(2));
            HidePanel();
        });

        PassiveIncomeIntervalBonusBuyButton.onClick.AddListener(() => {
            GameController.StartActiveIncomeBonusCoroutine(BonusDictionary.GetBonusByKey(3));
            HidePanel();
        });

        SetBonusesDescriptions();
        SetBonusesPrices();
    }

    private void DefineInteractibleButtons()
    {
        ActiveIncomeBonusBuyButton.interactable = GameController.CanBonusBeUsed(BonusDictionary.GetBonusByKey(1));
        PassiveIncomeBonusBuyButton.interactable = GameController.CanBonusBeUsed(BonusDictionary.GetBonusByKey(2));
        PassiveIncomeIntervalBonusBuyButton.interactable = GameController.CanBonusBeUsed(BonusDictionary.GetBonusByKey(3));
    }

    private void SetBonusesDescriptions()
    {
        ActiveIncomeBonusDescription.text = BonusDictionary.GetBonusByKey(1).Description;
        PassiveIncomeBonusDescription.text = BonusDictionary.GetBonusByKey(2).Description;
        PassiveIncomeIntervalBonusDescription.text = BonusDictionary.GetBonusByKey(3).Description;
    }

    private void SetBonusesPrices()
    {
        ActiveIncomeBonusPrice.text = string.Format("{0}$",BonusDictionary.GetBonusByKey(1).BonusCost.ToString("0.00"));
        PassiveIncomeBonusPrice.text = string.Format("{0}$", BonusDictionary.GetBonusByKey(2).BonusCost.ToString("0.00"));
        PassiveIncomeIntervalBonusPrice.text = string.Format("{0}$", BonusDictionary.GetBonusByKey(3).BonusCost.ToString("0.00"));
    }
}
