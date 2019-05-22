using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outcome
{
    public float ProbabilityOfOutcome;
    public int ValueOfChangeInFunds;
    public bool IsValueAbsolute;
    public string Description;

    public Outcome(float probability, int valueOfChangeInFunds, string description, bool isValueAbsolute)
    {
        ProbabilityOfOutcome = probability;
        ValueOfChangeInFunds = valueOfChangeInFunds;
        Description = description;
        IsValueAbsolute = isValueAbsolute;
    }
}

public class Option
{
    public string OptionName;
    public List<Outcome> Outcomes;

    public Option(string optionName, List<Outcome> outcomes)
    {
        OptionName = optionName;
        Outcomes = outcomes;
    }
}

public class Event
{
    public string Title;
    public string Description;
    public int ProbabilityOfHappeningDuringOneSecondPeriodInPercent;
    public List<Option> Options;

    public Event(string title, int probablilityOfHappeningDuringOneSecondPeriodInPercent, List<Option> options, string description)
    {
        Title = title;
        Description = description;
        ProbabilityOfHappeningDuringOneSecondPeriodInPercent = probablilityOfHappeningDuringOneSecondPeriodInPercent;
        Options = options;
    }
}
