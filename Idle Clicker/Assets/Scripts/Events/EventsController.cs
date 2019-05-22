using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class Range
{
    public int Min, Max;

    public Range(int min, int max)
    {
        Min = min;
        Max = max;
    }

    public bool IsInsideRange(float number)
    {
        return number >= Min && number <= Max;
    }
}

public class EventsController : MonoBehaviour
{
    private EventDictionary EventDictionary = new EventDictionary();
    private List<Range> EventsProbabilityRangesList = new List<Range>();

    [SerializeField]
    private EventsPopupController EventsPopupController;
    [SerializeField]
    private EventResultPopupController EventResultPopupController;

    private void Start()
    {
        SetRangesForEventsProbability();
        StartEventsCoroutine();
    }

    private void StartEventsCoroutine()
    {
        StartCoroutine(RandomEventCoroutine());
    }

    private IEnumerator RandomEventCoroutine()
    {
        TryToLaunchEvent();
        yield return new WaitForSeconds(1);
        StartEventsCoroutine();
    }

    public void TryToLaunchEvent()
    {
        var randomizedValue = Random.Range(0, 100);

        if (EventsProbabilityRangesList.Where(e => e.IsInsideRange(randomizedValue)).Any())
        {
            var indexOfEventToLaunch = EventsProbabilityRangesList.FindIndex(e => e.IsInsideRange(randomizedValue));
            EventsPopupController.ShowEventPopup(EventDictionary.Events[indexOfEventToLaunch]);
        }
    }

    public void SetRangesForEventsProbability()
    {
        for(int i =0; i<EventDictionary.Events.Count; i++)
        {
            if (i == 0)
            {
                EventsProbabilityRangesList.Add(new Range(0, EventDictionary.Events[i].ProbabilityOfHappeningDuringOneSecondPeriodInPercent));
            }
            else
            {
                EventsProbabilityRangesList.Add(new Range(EventsProbabilityRangesList[i-1].Min + EventDictionary.Events[i].ProbabilityOfHappeningDuringOneSecondPeriodInPercent, EventsProbabilityRangesList[i - 1].Max + EventDictionary.Events[i].ProbabilityOfHappeningDuringOneSecondPeriodInPercent));
            }
        }
    }

    public List<Range> GetRangesForEventOptionsOutcomes(Option option, List<Range> ranges)
    {
        for (int i = 0; i < option.Outcomes.Count; i++)
        {
            if (i == 0)
            {
                ranges.Add(new Range(0, (int)option.Outcomes[i].ProbabilityOfOutcome));
            }
            else
            {
                ranges.Add(new Range(ranges[i - 1].Min + (int)option.Outcomes[i].ProbabilityOfOutcome, ranges[i - 1].Max + (int)option.Outcomes[i].ProbabilityOfOutcome));
            }
        }

        return ranges;
    }

    public void DefineOutcome(Option option)
    {
        List<Range> optionRanges = new List<Range>();
        var randomizedValue = Random.Range(0, 100);

        optionRanges = GetRangesForEventOptionsOutcomes(option, optionRanges);

        if (optionRanges.Where(e => e.IsInsideRange(randomizedValue)).Any())
        {
            var indexOfOutcome = optionRanges.FindIndex(e => e.IsInsideRange(randomizedValue));
            EventResultPopupController.ShowEventOutcomePopup(option.Outcomes[indexOfOutcome]);
        }
    }
}