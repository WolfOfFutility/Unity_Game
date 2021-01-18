using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObjective : Objective
{
    public List<GameObject> killTargets { get; set; }

    public KillObjective()
    {
        // Default
    }

    public KillObjective(int ob, List<GameObject> kt)
    {
        this.ObjectiveID = ob;
        this.killTargets = kt;
    }
}
