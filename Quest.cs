using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string QuestName { get; set; }
    public int LevelRequired { get; set; }
    public string QuestDescription { get; set; }

    public List<Objective> ObjectiveList { get; set; }

    public Quest()
    {
        // Default
    }

    // Bare Minimum Quest
    public Quest(string qn, int lr, string qd, List<Objective> ol)
    {
        this.QuestName = qn;
        this.LevelRequired = lr;
        this.QuestDescription = qd;
        this.ObjectiveList = ol;
    }
}
