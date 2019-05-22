using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventDictionary
{
    public Dictionary<int, Event> Events = new Dictionary<int, Event>()
    {
        { 0, new Event("Tax time",1,
            new List<Option>(){
                new Option("Take the risk", new List<Outcome>(){
                    new Outcome(60,0,"Luckily for us, it looks like the IRS didn’t find out about our tax fraud! We have saved 20% of the money!",true),
                    new Outcome(40,-40, "The IRS has detected that we want to avoid paying taxes! Now we not only have to pay the taxes, but also pay a fine!",false)
                }),
                new Option("Just pay taxes", new List<Outcome>(){
                    new Outcome(100,-20,"We are good citizens paying taxes in our country. That’s what everyone should do!",false)
                })
            },
            "Our accountant says we can save some money by skipping paying taxes - we just have to move the money to a bank account in another country and show the IRS we have 0 income. But it may not have a happy ending if the IRS finds out. Do we want to take the risk?"
            )
        },
        { 1, new Event("Invest in a business",2,
            new List<Option>(){
                new Option("Invest the money", new List<Outcome>() {
                    new Outcome(50,100000,"You have a nose for business! The company is a great market success and we have already earned some money from it! A great day for us!",true),
                    new Outcome(50, -20,"\"You win some, you lose some\" people say. It is not a good day for us, the company has gone bankrupt and we have lost the invested money.",false)
                }),
                new Option("Do not invest the money", new List<Outcome>(){
                    new Outcome(30,0, "You have a nose for business! The company has gone bankrupt shortly after we wanted to invest in it. All investors have lost their money!", true),
                    new Outcome(70,0, "The company has turned into a great market success! What a pity we haven’t invested in it, but on the other hand - at least we didn’t lose any money…",true)
                })
            },
            "People say there is an opportunity to invest in a new hot startup. Some say it is easy money - no chance we can lose on the transaction, but others say it will go bankrupt faster than you can blink. Do we want to invest in it?"
            )
        }
    };
}