using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Item
{
    public float ItemArmour { get; set; }

    public Helmet()
    {
        // Default
    }

    public Helmet(string itemName, float armour, string rarity, int lr, string desc)
    {
        this.ItemName = itemName;
        this.ItemRarity = rarity;
        this.LevelRequired = lr;
        this.ItemDescription = desc;
        this.ItemArmour = armour;
    }
} 
