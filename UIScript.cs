using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("QuestsPanel").SetActive(false);
        GameObject.Find("InventoryPanel").SetActive(false);
        GameObject.Find("SkillTreePanel").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
