using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventsPopupController : MonoBehaviour
{
    [SerializeField]
    private Button FirstOptionButton;
    [SerializeField]
    private Button SecondOptionButton;

    [SerializeField]
    private TextMeshProUGUI EventDescription;
    [SerializeField]
    private TextMeshProUGUI FirstOptionButtonText;
    [SerializeField]
    private TextMeshProUGUI SecondOptionButtonText;

    [SerializeField]
    private EventsController EventsController;

    public void ShowEventPopup(Event eventToLaunch)
    {
        gameObject.SetActive(true);
        SetTexts(eventToLaunch);
        BindButtons(eventToLaunch);
        Time.timeScale = 0;
    }

    private void CloseEventPopup()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void SetTexts(Event eventToLaunch)
    {
        EventDescription.text = eventToLaunch.Description;
        FirstOptionButtonText.text = eventToLaunch.Options.First().OptionName;
        SecondOptionButtonText.text = eventToLaunch.Options.Last().OptionName;
    }

    private void BindButtons(Event eventToLaunch)
    {
        FirstOptionButton.onClick.RemoveAllListeners();
        SecondOptionButton.onClick.RemoveAllListeners();

        FirstOptionButton.onClick.AddListener(()=> { EventsController.DefineOutcome(eventToLaunch.Options.First()); CloseEventPopup(); });
        SecondOptionButton.onClick.AddListener(() => { EventsController.DefineOutcome(eventToLaunch.Options.Last()); CloseEventPopup(); });
    }
}
