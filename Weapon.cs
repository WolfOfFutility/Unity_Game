using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
    public float ItemDamage { get; set; }
    public float ItemRange { get; set; }

    public Weapon()
    {
        // Default
    }

    public Weapon(string itemName, float damage, float range, string rarity, int lr, string desc)
    {
        this.ItemName = itemName;
        this.ItemRarity = rarity;
        this.LevelRequired = lr;
        this.ItemDescription = desc;
        this.ItemDamage = damage;
        this.ItemRange = range;
    }
}
