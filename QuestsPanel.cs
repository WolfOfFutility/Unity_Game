using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestsPanel : MonoBehaviour
{
    public GameObject QuestListItem;
    public GameObject QuestDetailsText;
    private List<Quest> questList = new List<Quest>();
    private bool openingQuestList = false;
    private float lastYPos = 174f;
    //private int currentID = 0;
    

    public void SetQuestPanelList(List<Quest> list)
    {
        questList = list;
        openingQuestList = true;
    }

    private void ShowQuestDetails()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }

        if (openingQuestList)
        {
            foreach(Quest q in questList)
            {
                
                GameObject newItem = Instantiate(QuestListItem, new Vector3(), Quaternion.identity);
                newItem.transform.SetParent(this.gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform);
                newItem.transform.GetChild(0).GetComponent<Text>().text = q.QuestName;
                newItem.GetComponent<Button>().onClick.AddListener(() =>
                {
                    QuestDetailsText.GetComponent<Text>().text = q.QuestDescription;
                });
            }

            openingQuestList = false;
        }
    }
}
