using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC1Talk : MonoBehaviour
{
    public GameObject NPC;
    public GameObject player;

    private Interaction npcScript;
    private TextMesh missionPing;
    private Dialog dialog = new Dialog("Testing Character 1", "Testing Class", "Hello There, Adventurer!");
    private List<Quest> questList = new List<Quest>();

    private GameObject dialogBox;
    private GameObject dialogTitle;
    private GameObject dialogContent;
    private GameObject dialogOptions;

    private bool isTalking = false;

    // Start is called before the first frame update
    void Start()
    {
        List<Objective> oblist = new List<Objective>();
        List<GameObject> targets = new List<GameObject>();
        targets.Add(GameObject.Find("Enemy_1"));
        oblist.Add(new KillObjective(1, targets));

        List<Objective> oblist2 = new List<Objective>();
        List<GameObject> targets2 = new List<GameObject>();
        targets2.Add(GameObject.Find("Enemy_2"));
        targets2.Add(GameObject.Find("Enemy_3"));
        oblist2.Add(new KillObjective(2, targets2));

        questList.Add(new Quest("Testing Quest 1", 1, "Destroy an enemy over there.", oblist));
        questList.Add(new Quest("Testing Quest 2", 3, "Destroy the rest of the enemies over there.", oblist2));
        npcScript = NPC.GetComponent<Interaction>();
        missionPing = NPC.transform.GetChild(1).gameObject.GetComponent<TextMesh>();

        dialogBox = npcScript.dialogBox;
        dialogBox.SetActive(false);

        dialogTitle = npcScript.dialogBox.transform.GetChild(0).gameObject;
        dialogContent = npcScript.dialogBox.transform.GetChild(1).gameObject;
        dialogOptions = npcScript.dialogBox.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(questList.Count >= 1)
        {
            if(player.GetComponent<Player>().GetPlayerLevel() >= questList[0].LevelRequired)
            {
                missionPing.text = "!";
            }
        }
        else
        {
            missionPing.text = "";
        }

        if(npcScript.talkingTrigger)
        {
            Debug.Log(dialog.Message);
            dialogTitle.GetComponent<Text>().text = dialog.CharacterName;
            dialogContent.GetComponent<Text>().text = dialog.Message;
            dialogOptions.GetComponent<Text>().text = "Press Y to accept.";

            dialogBox.SetActive(true);
            npcScript.talkingTrigger = false;
            isTalking = true;
        }

        if(isTalking && npcScript.acceptingTrigger)
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();

            if(player.CheckIfCompleted(questList[0]))
            {
                player.AddToPlayerExp(200f);
                dialogContent.GetComponent<Text>().text = questList[0].QuestName + " has been Completed.";
                player.RemoveQuestFromCompleted(questList[0]);
                questList.RemoveAt(0);
                player.SetDisplayInfo(true);
            }

            if(questList.Count > 0)
            {
                if(player.GetComponent<Player>().GetPlayerLevel() >= questList[0].LevelRequired)
                {
                    player.AddToAcceptedQuests(questList[0]);
                    dialogContent.GetComponent<Text>().text = questList[0].QuestName + " has been accepted.";
                    player.SetDisplayInfo(true);
                }
            }

            npcScript.acceptingTrigger = false;
            isTalking = false;
        }

        if(dialogBox.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                dialogBox.SetActive(false);
            }
        }
    }
}
