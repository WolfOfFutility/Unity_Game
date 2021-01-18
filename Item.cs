using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string ItemName { get; set; }
    public string ItemRarity { get; set; }
    public int LevelRequired { get; set; }
    public string ItemDescription { get; set; }

    public Item()
    {
        // Default
    }

    public Item(string itemName, string rarity, int lr, string desc)
    {
        this.ItemName = itemName;
        this.ItemRarity = rarity;
        this.LevelRequired = lr;
        this.ItemDescription = desc;
    }
}
