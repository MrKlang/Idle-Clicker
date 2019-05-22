using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventResultPopupController : MonoBehaviour
{
    [SerializeField]
    private Button OkButton;

    [SerializeField]
    private TextMeshProUGUI ResultText;

    [SerializeField]
    private EventsController EventsController;
    [SerializeField]
    private GameController GameController;

    public void ShowEventOutcomePopup(Outcome outcome)
    {
        gameObject.SetActive(true);
        ResultText.text = outcome.Description;
        OkButton.onClick.RemoveAllListeners();
        OkButton.onClick.AddListener(()=> { GameController.ModifyMoneyAmountOnEventCompletion(outcome.ValueOfChangeInFunds,outcome.IsValueAbsolute); CloseEventResultPopup(); });
        Time.timeScale = 0;
    }

    private void CloseEventResultPopup()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
