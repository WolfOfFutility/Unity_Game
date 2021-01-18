using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private List<Item> itemList = new List<Item>();
    private System.Random rand = new System.Random();
    private System.Random secondRand = new System.Random();

    private List<string> itemPrefixes = new List<string>();
    private List<string> itemSuffixes = new List<string>();
    private List<string> weaponTypesList = new List<string>();
    private List<string> helmetTypesList = new List<string>();
    private List<string> chestplateTypesList = new List<string>();
    private List<string> legsTypesList = new List<string>();
    private List<string> bootsTypesList = new List<string>();
    private List<string> consTypesList = new List<string>();

    private List<string> itemTypes = new List<string>();

    public Item GetARandomItem(string rarity, int playerLevel)
    {
        Item item = new Item();

        List<Item> filteredList = itemList.Where(x => x.ItemRarity == rarity && x.LevelRequired <= playerLevel).ToList();
        int num = rand.Next(filteredList.Count);
        item = filteredList[num];

        return item;
    }

    private float GetWeaponTypeRange(string weaponType)
    {
        switch(weaponType)
        {
            case "Sceptre":
                return 2f;
            case "Sword":
                return 2f;
            case "Lance":
                return 3f;
            case "Dagger":
                return 1.5f;
            case "Rapier":
                return 2f;
            case "Falchion":
                return 2f;
            case "Broadsword":
                return 2.5f;
            case "Mace":
                return 1.8f;
            case "Crossbow":
                return 5f;
            case "Longbow":
                return 5.5f;
            case "Shortbow":
                return 4.8f;
            default:
                return 0f;
        }
    }

    public Item GenerateARandomItem(string rarity, int playerLevel)
    {
        Item item = new Item();
        float rarityMult = 1;

        switch(rarity)
        {
            case "Common":
                rarityMult = 1f;
                break;
            case "Uncommon":
                rarityMult = 1.2f;
                break;
            case "Rare":
                rarityMult = 1.4f;
                break;
            case "Unique":
                rarityMult = 2f;
                break;
            case "Epic":
                rarityMult = 5f;
                break;
        }

        float numberAmount = (float)(10 * (((0.1 * playerLevel) * rarityMult) + 1));

        int randomItemTypeIndex = rand.Next(itemTypes.Count);

        switch(itemTypes[randomItemTypeIndex])
        {
            case "Weapon":
                string weaponType = weaponTypesList[secondRand.Next(weaponTypesList.Count)];
                float range = GetWeaponTypeRange(weaponType);

                item = new Weapon(
                    itemPrefixes[secondRand.Next(itemPrefixes.Count)] + weaponType + itemSuffixes[secondRand.Next(itemSuffixes.Count)],
                    numberAmount,
                    range,
                    rarity,
                    playerLevel,
                    "A weapon created at random."
                    );
                break;
            case "Helmet":
                item = new Helmet(
                    itemPrefixes[secondRand.Next(itemPrefixes.Count)] + helmetTypesList[secondRand.Next(helmetTypesList.Count)] + itemSuffixes[secondRand.Next(itemSuffixes.Count)],
                    numberAmount,
                    rarity,
                    playerLevel,
                    "A helmet created at random."
                    );
                break;
            case "Chestplate":
                item = new Chestplate(
                    itemPrefixes[secondRand.Next(itemPrefixes.Count)] + chestplateTypesList[secondRand.Next(chestplateTypesList.Count)] + itemSuffixes[secondRand.Next(itemSuffixes.Count)],
                    numberAmount,
                    rarity,
                    playerLevel,
                    "A chestplate created at random."
                    );
                break;
            case "Legs":
                item = new Legs(
                    itemPrefixes[secondRand.Next(itemPrefixes.Count)] + legsTypesList[secondRand.Next(legsTypesList.Count)] + itemSuffixes[secondRand.Next(itemSuffixes.Count)],
                    numberAmount,
                    rarity,
                    playerLevel,
                    "A pair of leggings created at random."
                    );
                break;
            case "Boots":
                item = new Boots(
                    itemPrefixes[secondRand.Next(itemPrefixes.Count)] + bootsTypesList[secondRand.Next(bootsTypesList.Count)] + itemSuffixes[secondRand.Next(itemSuffixes.Count)],
                    numberAmount,
                    rarity,
                    playerLevel,
                    "A pair of boots created at random."
                    );
                break;
            case "Consumable":
                item = new Consumable(
                    itemPrefixes[secondRand.Next(itemPrefixes.Count)] + consTypesList[secondRand.Next(consTypesList.Count)] + itemSuffixes[secondRand.Next(itemSuffixes.Count)],
                    numberAmount,
                    rarity,
                    playerLevel,
                    "A consumable created at random."
                    );
                break;
            default:
                item = new Item();
                break;
        }

        return item;
    }

    private void PreloadPrefixes()
    {
        itemPrefixes.Add("Red ");
        itemPrefixes.Add("White ");
        itemPrefixes.Add("Black ");
        itemPrefixes.Add("Heavy ");
        itemPrefixes.Add("Light ");
        itemPrefixes.Add("Savage ");
        itemPrefixes.Add("Blighted ");
        itemPrefixes.Add("Cursed ");
        itemPrefixes.Add("Blessed ");
        itemPrefixes.Add("Grotesque ");
        itemPrefixes.Add("Adventurer's ");
        itemPrefixes.Add("Knight's ");
        itemPrefixes.Add("King's ");
        itemPrefixes.Add("Queen's ");
    }

    private void PreloadSuffixes()
    {
        itemSuffixes.Add(" of Angelic Glory");
        itemSuffixes.Add(" of Bloody Sacrifice");
        itemSuffixes.Add(" of Starlight");
        itemSuffixes.Add(" of Hellfire");
        itemSuffixes.Add(" of Redemption");
        itemSuffixes.Add(" of Justice");
        itemSuffixes.Add(" of Revenge");
        itemSuffixes.Add(" of Havoc");
        itemSuffixes.Add("");
        itemSuffixes.Add("");
        itemSuffixes.Add("");
    }

    private void PreloadWeaponTypes()
    {
        weaponTypesList.Add("Sceptre");
        weaponTypesList.Add("Sword");
        weaponTypesList.Add("Lance");
        weaponTypesList.Add("Dagger");
        weaponTypesList.Add("Rapier");
        weaponTypesList.Add("Falchion");
        weaponTypesList.Add("Broadsword");
        weaponTypesList.Add("Mace");
        weaponTypesList.Add("Crossbow");
        weaponTypesList.Add("Longbow");
        weaponTypesList.Add("Shortbow");
    }

    private void PreloadHelmetTypes()
    {
        helmetTypesList.Add("Armet");
        helmetTypesList.Add("Helm");
        helmetTypesList.Add("Combat Helmet");
        helmetTypesList.Add("Horned Helmet");
    }

    private void PreloadChestTypes()
    {
        chestplateTypesList.Add("Chainmail");
        chestplateTypesList.Add("Platebody");
        chestplateTypesList.Add("Cuirass");
        chestplateTypesList.Add("Ringmail");
        chestplateTypesList.Add("Tunic");
    }

    private void PreloadLegsTypes()
    {
        legsTypesList.Add("Trousers");
        legsTypesList.Add("Platelegs");
        legsTypesList.Add("Leggings");
        legsTypesList.Add("Breeches");
        legsTypesList.Add("Stockings");
    }

    private void PreloadBootsTypes()
    {
        bootsTypesList.Add("Sandals");
        bootsTypesList.Add("Plated Boots");
        bootsTypesList.Add("Chain Boots");
        bootsTypesList.Add("Leather Boots");
    }

    private void PreloadConsumableTypes()
    {
        consTypesList.Add("Potion");
        consTypesList.Add("Vial");
        consTypesList.Add("Sip");
        consTypesList.Add("Bread");
    }


    // Start is called before the first frame update
    void Start()
    {
        itemTypes.Add("Weapon");
        itemTypes.Add("Helmet");
        itemTypes.Add("Chestplate");
        itemTypes.Add("Legs");
        itemTypes.Add("Boots");
        itemTypes.Add("Consumable");

        PreloadBootsTypes();
        PreloadChestTypes();
        PreloadConsumableTypes();
        PreloadHelmetTypes();
        PreloadLegsTypes();
        PreloadPrefixes();
        PreloadSuffixes();
        PreloadWeaponTypes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
