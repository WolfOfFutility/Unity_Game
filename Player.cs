using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ** NOTES ** 

// ** DATABASE THINGS **
    // Save Last Player Checkpoint to DB
    // Save Player Info to DB
    // Read Player Locations in DB
    // Read Player Info in DB

// ** DESIGN THINGS **
    // Create Better Sprites
    // Create Animations
    // create Better UI

// ** STORY THINGS **
    // Create a plot
    // Create Dialogs
    // Create new Quests
    // Create new Scenes / Make it an open world
    // Create item names
    // Create skill names

// ** DEVELOPMENT THINGS ** 
    // Fix player movement onto elevated surfaces
    // Create a more advanced combat system
    // Create a skill system


public class Player : MonoBehaviour
{
    public GameObject player;
    public GameObject playerUI;
    public GameObject questsPanel;
    private GameObject bottomPanel;
    private GameObject recentlyKilled;

    private float moveSpeed = 1f;
    private float sensitivity = 4f;
    private float playerDamage = 10f;
    private float playerAttackSpeed = 3f;
    private float playerEXP = 0f;
    private float playerArmour = 100f;
    private float playerHP = 100f;
    private float playerMaxHP = 100f;
    private float levelingThreshold = 100;
    private float critStrikeChance = 10f;
    private float damageFromEquip = 0f;
    private float armourFromEquip = 0f;
    private float playerRange = 4f;
    //private float speedFromEquip = 0f;

    private int playerLevel = 1;

    private string questListMessage = "";

    private bool recentKill = false;
    private bool displayInfo = false;
    private bool movingRight = false;
    private bool movingLeft = false;
    private bool movingForward = false;
    private bool movingBack = false;
    private bool jumping = false;

    private List<Quest> acceptedQuests = new List<Quest>();
    private List<Quest> completedQuests = new List<Quest>();
    private List<Item> playerInventory = new List<Item>();
    private List<Item> playerEquipped = new List<Item>();
    
    // **PLAYER MOVEMENT**

    // The player moves right
    private void MoveRight(GameObject p)
    {
        p.transform.Translate(Vector3.right * moveSpeed);
    }

    // The player moves left
    private void MoveLeft(GameObject p)
    {
        p.transform.Translate(Vector3.left * moveSpeed);
    }

    // The player moves forward
    private void MoveForward(GameObject p)
    {
        p.transform.Translate(Vector3.forward * moveSpeed);
    }

    // The player moves backward
    private void MoveBackward(GameObject p)
    {
        p.transform.Translate(Vector3.back * moveSpeed);
    }

    // The player jumps
    private void Jump(GameObject p)
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.VelocityChange);
    }

    //**PLAYER VARIABLES AND INTERACTIONS**

    // Check if anything has been satisfied in the quests that the player currently has
    void CheckKills()
    {
        foreach (Quest x in acceptedQuests)
        {
            questListMessage += x.QuestName + "\n";

            foreach (KillObjective y in x.ObjectiveList)
            {
                foreach (GameObject target in y.killTargets)
                {
                    if (target == recentlyKilled)
                    {
                        List<GameObject> newTargets = y.killTargets;
                        newTargets.Remove(target);

                        y.killTargets = newTargets;

                        if (y.killTargets.Count == 0)
                        {
                            List<Quest> currentAccepted = acceptedQuests;

                            completedQuests.Add(x);
                            currentAccepted.Remove(x);

                            acceptedQuests = currentAccepted;

                            Debug.Log(x.QuestName + " Completed!");
                        }
                    }
                }
            }
        }

        displayInfo = true;
    }

    public float GetPlayerAttackRange()
    {
        return playerRange;
    }

    // Respawns at the last checkpoint
    void RespawnAtCheckpoint()
    {
        player.transform.position = new Vector3(0, 1f, -23f);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    // Displays Quest Info on the UI
    void DisplayQuestInfo()
    {
        foreach(Quest x in acceptedQuests)
        {
            questListMessage += x.QuestName + "\n";
        }


        player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = questListMessage;
        displayInfo = false;
        questListMessage = "";
    }

    // Returns all of the items in a players inventory
    public List<Item> GetPlayerInventory()
    {
        return playerInventory;
    }

    // Adds an Item to the inventory
    public void AddToInventory(Item item)
    {
        playerInventory.Add(item);
    }

    // Removes an Item from the inventory
    public void RemoveFromInventory(Item item)
    {
        playerInventory.Remove(item);
    }

    // Return the quests that the player has accepted
    public List<Quest> GetPlayerQuests()
    {
        return acceptedQuests;
    }

    // Return the quests that the player has completed
    public List<Quest> GetPlayerCompletedQuests()
    {
        return completedQuests;
    }

    // Adds a new quest to the players accepted quest list
    public void AddToAcceptedQuests(Quest newQuest)
    {
        acceptedQuests.Add(newQuest);
        questsPanel.GetComponent<QuestsPanel>().SetQuestPanelList(acceptedQuests);
    }

    // Adds a quests to the recently completed quest list (ready to be handed in)
    public void AddToCompletedQuests(Quest finishedQuest)
    {
        completedQuests.Add(finishedQuest);
    }

    // Removes a quest from the completed list (handed in for rewards) 
    public void RemoveQuestFromCompleted(Quest quest)
    {
        completedQuests.Remove(quest);
    }

    // Removes a quest from the accepted list (abandoned or completed)
    public void RemoveQuestFromAccepted(Quest quest)
    {
        acceptedQuests.Remove(quest);
    }

    // Check if a quest is completed
    public bool CheckIfCompleted(Quest quest)
    {
        bool complete = false;

        if(completedQuests.Contains(quest))
        {
            complete = true;
        }

        return complete;
    }

    // Set displaying the updated info on the UI
    public void SetDisplayInfo(bool b)
    {
        displayInfo = b;
    }

    // Get the player's current level
    public int GetPlayerLevel()
    {
        return playerLevel;
    }

    // Get the player's current damage
    public float GetPlayerDamage()
    {
        return playerDamage;
    }

    // Get the player's current attack speed
    public float GetPlayerAttackSpeed()
    {
        return playerAttackSpeed;
    }

    // Get the player's current crit chance
    public float GetPlayerCritChance()
    {
        return critStrikeChance;
    }

    // Get the player's current HP
    public float GetPlayerHP()
    {
        return playerHP;
    }

    // Player takes damage
    public void TakeFromPlayerHP(float damageAmount)
    {
        playerHP -= (float)(damageAmount - (0.33 * (playerArmour + armourFromEquip)));
    }

    // Player heals an amount
    public void HealPlayer(float healAmount) {
        playerHP += healAmount;
    }

    // Get the player's armour
    public float GetPlayerArmour()
    {
        return playerArmour;
    }

    // See if the player has recently killed something
    public bool GetRecentKillStatus()
    {
        return recentKill;
    }

    // Set if a player has recently killed something
    public void SetRecentKillStatus(bool status)
    {
        recentKill = status;
    }

    // Set what the player has recently killed
    public void SetRecentKillObject(GameObject recentKillObject)
    {
        recentlyKilled = recentKillObject;
    }

    // Get whatever the player has recently killed
    public GameObject GetRecentKillObject()
    {
        return recentlyKilled;
    }

    // Get the player's current Exp
    public float GetPlayerExp()
    {
        return playerEXP;
    }

    // Add EXP to the player's EXP pool
    public void AddToPlayerExp(float expGain)
    {
        playerEXP += expGain;
    }

    // Equip a new item to the player, recalculate the amount of damage that is added
    public void EquipNewItem(string slot, Item item)
    {
        foreach (Item i in playerEquipped)
        {
            if(i.GetType().Name == slot)
            {
                playerEquipped.Remove(i);
                break;
            }
        }

        playerEquipped.Add(item);
        CalculateItemsBonuses();
    }

    // Calculate the Bonuses from all equipped items on the player
    private void CalculateItemsBonuses()
    {
        damageFromEquip = 0f;
        armourFromEquip = 0f;
        playerRange = 4f;

        foreach (Item i in playerEquipped)
        {
            switch (i.GetType().Name)
            {
                case "Weapon":
                    damageFromEquip += ((Weapon)i).ItemDamage;
                    playerRange += ((Weapon)i).ItemRange;
                    break;
                case "Helmet":
                    armourFromEquip += ((Helmet)i).ItemArmour;
                    break;
                case "Chestplate":
                    armourFromEquip += ((Chestplate)i).ItemArmour;
                    break;
                case "Boots":
                    armourFromEquip += ((Boots)i).ItemArmour;
                    break;
                case "Consumable":
                    armourFromEquip += ((Consumable)i).ItemArmour;
                    break;
            }
        }
    }

    // return the amount of extra damage that the player gets from items
    public float GetPlayerEquipDamage()
    {
        return damageFromEquip;
    }

    // return the amount of extra armour the player gets from items
    public float GetPlayerEquipArmour()
    {
        return armourFromEquip;
    }

    // Level the player up
    private void LevelUp()
    {
        playerLevel += 1;
        playerMaxHP *= 1.1f;
        GameObject.Find("PlayerHealthBar").gameObject.GetComponent<Slider>().maxValue = playerMaxHP;
        playerEXP -= levelingThreshold;
        levelingThreshold *= 1.2f;
        playerDamage *= 1.5f;
        playerAttackSpeed *= 1.01f;
        critStrikeChance *= 1.01f;
        playerHP = playerMaxHP;
        playerUI.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = playerLevel.ToString();
        Debug.Log("Level up to level " + playerLevel);
    }


    // **GAME RUNTIME**

    // Start is called before the first frame update
    void Start()
    {
        CalculateItemsBonuses();
        GameObject.Find("PlayerHealthBar").gameObject.GetComponent<Slider>().maxValue = playerMaxHP;
        GameObject.Find("PlayerHealthBar").gameObject.GetComponent<Slider>().value = playerMaxHP;

        // Testing God Weapon
        //AddToInventory(new Weapon("Bulwark of Ceejay The Mighty", 100000f, "Common", 1, "A mighty weapon that is sure to slay " +
                        // "all manner of foes with a single strike. To be used with caution. Best not to look at it too closely, fables tell of its penchat " +
                        // "for eating the souls of its users."));
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("PlayerHealthBar").gameObject.GetComponent<Slider>().value = playerHP;
        
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            movingRight = true;
        }

        if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            movingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            movingLeft = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            movingLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            movingForward = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            movingForward = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            movingBack = true;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            movingBack = false;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
        }

        if(player.transform.position.y < -50)
        {
            RespawnAtCheckpoint();
        }

        if(recentKill)
        {
            CheckKills();

            recentKill = false;
        }

        if(playerEXP >= levelingThreshold)
        {
            LevelUp();
        }

        if(playerHP < 0)
        {
            playerHP = playerMaxHP;
            RespawnAtCheckpoint();
        }

        if(playerHP > playerMaxHP)
        {
            playerHP = playerMaxHP;
        }
    }

    private void FixedUpdate()
    {
        if(movingRight)
        {
            MoveRight(player);
        }

        if(movingLeft)
        {
            MoveLeft(player);
        }

        if(movingForward)
        {
            MoveForward(player);
        }

        if(movingBack)
        {
            MoveBackward(player);
        }

        if(jumping)
        {
            Jump(player);
            jumping = false;
        }

        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");
        transform.RotateAround(player.transform.position, Vector3.up, rotateHorizontal * sensitivity); //use transform.Rotate(-transform.up * rotateHorizontal * sensitivity) instead if you dont want the camera to rotate around the player
    }
}
