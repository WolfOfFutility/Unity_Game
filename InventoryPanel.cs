using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{

    public GameObject InventoryItem;
    public GameObject InventoryItemTitleText;
    public GameObject InventoryDetailsPanel;
    public GameObject ItemDamageText;
    public GameObject ItemLevelText;
    public GameObject ItemRarityText;
    public GameObject Player;
    public GameObject equipButton;
    public GameObject discardButton;
    private Player player;
    private List<Item> currentInventory = new List<Item>();
    private Item currentSelectedItem;
    private bool openInv = false;
    private bool openedBefore = false;

    private void LoadItemDetails(Item i)
    {
        InventoryItemTitleText.GetComponent<Text>().text = i.ItemName;
        InventoryDetailsPanel.GetComponent<Text>().text = i.ItemDescription;

        switch (i.GetType().Name)
        {
            case "Weapon":
                ItemDamageText.GetComponent<Text>().text = $"{((Weapon)i).ItemDamage} dps";
                break;
            case "Helmet":
                ItemDamageText.GetComponent<Text>().text = $"{((Helmet)i).ItemArmour} armour";
                break;
            case "Chestplate":
                ItemDamageText.GetComponent<Text>().text = $"{((Chestplate)i).ItemArmour} armour";
                break;
            case "Boots":
                ItemDamageText.GetComponent<Text>().text = $"{((Boots)i).ItemArmour} armour";
                break;
            case "Legs":
                ItemDamageText.GetComponent<Text>().text = $"{((Legs)i).ItemArmour} armour";
                break;
            case "Consumable":
                ItemDamageText.GetComponent<Text>().text = $"{((Consumable)i).ItemArmour} armour";
                break;
            default:
                Debug.Log("Item type not recognised");
                break;
        }

        ItemLevelText.GetComponent<Text>().text = $"Level {i.LevelRequired} required";
        ItemRarityText.GetComponent<Text>().text = i.ItemRarity;

        currentSelectedItem = i;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetComponent<Player>();

        equipButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Equip Button Clicked!");
            player.EquipNewItem("Testing Slot", currentSelectedItem);
            //equipButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Unequip";
        });

        discardButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Discard Button Clicked!");
            player.RemoveFromInventory(currentSelectedItem);
            currentSelectedItem = new Item();
            currentInventory = new List<Item>();

            InventoryItemTitleText.GetComponent<Text>().text = "";
            InventoryDetailsPanel.GetComponent<Text>().text = "";
            ItemDamageText.GetComponent<Text>().text = "";
            ItemLevelText.GetComponent<Text>().text = "";
            ItemRarityText.GetComponent<Text>().text = "";

            int numberOfChildren = this.gameObject.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).transform.childCount;
            this.gameObject.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).transform.DetachChildren();

            foreach (Item i in player.GetPlayerInventory())
            {
                GameObject newItem = Instantiate(InventoryItem, new Vector3(), Quaternion.identity);
                newItem.transform.SetParent(this.gameObject.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).transform);
                newItem.transform.GetChild(0).GetComponent<Text>().text = i.ItemName;
                newItem.GetComponent<Button>().onClick.AddListener(() =>
                {
                    LoadItemDetails(i);
                });

                currentInventory.Add(i);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }

        if(this.gameObject.activeSelf)
        {
            openInv = true;
        }
        else
        {
            openInv = false;
            openedBefore = false;
        }

        if(openInv && !openedBefore)
        {
            foreach(Item i in player.GetPlayerInventory())
            {
                if(!currentInventory.Contains(i))
                {
                    GameObject newItem = Instantiate(InventoryItem, new Vector3(), Quaternion.identity);
                    newItem.transform.SetParent(this.gameObject.transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).transform);
                    newItem.transform.GetChild(0).GetComponent<Text>().text = i.ItemName;
                    newItem.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        LoadItemDetails(i);
                    });

                    currentInventory.Add(i);
                }
            }

            openInv = false;
            openedBefore = true;
        }
    }
}
