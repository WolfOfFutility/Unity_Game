using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject NPC;
    public GameObject player;
    public GameObject dialogBox;

    public bool talkingTrigger = false;
    public bool acceptingTrigger = false;
    public bool attackingTrigger = false;

    private float range = 5f;
    private float attackRange = 1f;

    // Start is called before the first frame update
    void Start()
    {
        attackRange = player.GetComponent<Player>().GetPlayerAttackRange();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > NPC.transform.position.x - range 
            && player.transform.position.z > NPC.transform.position.z - range
            && player.transform.position.x < NPC.transform.position.x + range
            && player.transform.position.z < NPC.transform.position.z + range)
        {

            if(Input.GetKeyDown(KeyCode.T))
            {
                talkingTrigger = true;
            }

            if(Input.GetKeyDown(KeyCode.Y))
            {
                acceptingTrigger = true;
            }

            if(Input.GetMouseButtonDown(0))
            {
                attackingTrigger = true;
            }

            if(Input.GetMouseButtonUp(0))
            {
                attackingTrigger = false;
            }
        }
        else
        {
            talkingTrigger = false;
            acceptingTrigger = false;
        }

        if (player.transform.position.x > NPC.transform.position.x - attackRange
            && player.transform.position.z > NPC.transform.position.z - attackRange
            && player.transform.position.x < NPC.transform.position.x + attackRange
            && player.transform.position.z < NPC.transform.position.z + attackRange)
        {

            if (Input.GetMouseButtonDown(0))
            {
                attackingTrigger = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                attackingTrigger = false;
            }

        }
        else
        {
            attackingTrigger = false;
        }
    }
}
