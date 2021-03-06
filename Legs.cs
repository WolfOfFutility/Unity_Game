﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : Item
{
    public float ItemArmour { get; set; }

    public Legs()
    {
        // Default
    }

    public Legs(string itemName, float armour, string rarity, int lr, string desc)
    {
        this.ItemName = itemName;
        this.ItemRarity = rarity;
        this.LevelRequired = lr;
        this.ItemDescription = desc;
        this.ItemArmour = armour;
    }
}
