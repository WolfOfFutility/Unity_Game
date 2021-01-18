using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyHealth;

    private float defencePoints;
    private float MaxHP = 100f;
    private float enemyDamage = 10f;
    private float enemyAttackSpeed = 5f;
    private float HP;
    private float timer = 0f;
    private float attackTimer = 0f;
    private Player player;
    private Interaction interaction;
    private System.Random rand = new System.Random();
    private GameObject dropPanel;

    private float spareRarity;

    private void DropRoll()
    {

        float roll = rand.Next(1001);

        if(roll == spareRarity)
        {
            roll = rand.Next(1001);
            spareRarity = roll;
        }

        string rarity = "";

        if(roll < 5)
        {
            rarity = "Epic";
            
        }
        else if(roll < 50 && roll > 5)
        {
            rarity = "Unique";
        }
        else if(roll < 150 && roll > 50)
        {
            rarity = "Rare";
        }
        else if(roll < 500 && roll > 150)
        {
            rarity = "Uncommon";
        }
        else
        {
            rarity = "Common";
        }

        
        Item item = GameObject.Find("Master").GetComponent<GameMaster>().GenerateARandomItem(rarity, player.GetPlayerLevel());
        Debug.Log($"{rarity} Drop - {item.ItemName}");
        player.AddToInventory(item);
        //DropNotification(rarity);
    }

    private void DropNotification(string dropType)
    {
        dropPanel.SetActive(true);
        dropPanel.transform.GetChild(0).GetComponent<Text>().text = $"New Drop! - {dropType}";

        dropPanel.transform.position = new Vector3(dropPanel.transform.position.x, -250f, dropPanel.transform.position.z);
        while(dropPanel.transform.position.y < 250)
        {
            dropPanel.transform.Translate(Vector3.up * 5);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        player = GameObject.Find("Player").GetComponent<Player>();
        interaction = enemy.GetComponent<Interaction>();
        enemyHealth = enemy.transform.GetChild(1).gameObject;
        enemyHealth.GetComponent<TextMesh>().text = "HP: " + HP;

        dropPanel = GameObject.Find("DropPanel");
        GameObject.Find("DropPanel").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += 0.01f * player.GetPlayerAttackSpeed();
        attackTimer += 0.01f * enemyAttackSpeed;


        if(timer > 1 && interaction.attackingTrigger)
        {
            float critRoll = rand.Next(101);
            
            if(critRoll <= player.GetPlayerCritChance())
            {
                HP -= (float)((player.GetPlayerDamage() * 1.5) - (defencePoints * 0.33) + player.GetPlayerEquipDamage());
                Debug.Log("CRITICAL HIT!");
            }
            else
            {
                HP -= (float)(player.GetPlayerDamage() - (defencePoints * 0.33) + player.GetPlayerEquipDamage());
            }
            
            enemyHealth.GetComponent<TextMesh>().text = "HP: " + Math.Round(HP);
            timer = 0f;
        }

        if(attackTimer > 1 && interaction.attackingTrigger)
        {
            float currentPlayerHp = player.GetPlayerHP();
            player.TakeFromPlayerHP(enemyDamage);
            attackTimer = 0f;
        }

        if(HP <= 0)
        {
            player.SetRecentKillObject(enemy);
            player.SetRecentKillStatus(true);
            player.AddToPlayerExp(MaxHP);
            Destroy(enemy);
            DropRoll();
        }
    }
}
