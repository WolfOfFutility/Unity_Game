using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2Talk : MonoBehaviour
{
    public GameObject NPC;
    private Interaction npcScript;
    private Dialog dialog = new Dialog("Testing Character 2", "Testing Class", "Get Lost!");

    // Start is called before the first frame update
    void Start()
    {
        npcScript = NPC.GetComponent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
        if(npcScript.talkingTrigger)
        {
            Debug.Log(dialog.Message);
            npcScript.talkingTrigger = false;
        }
    }
}
