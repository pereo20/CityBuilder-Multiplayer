using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class HeroData
{
    public string heroId;
    public string heroName;
    public HeroClass heroClass;
    public int level = 1;
    public int maxLevel = 50;
    public int experience = 0;
    public int experienceToNextLevel = 100;
    
    // Stats
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int attack = 10;
    public int defense = 5;
    public int speed = 8;
    public int mana = 50;
    public int manaRegeneration = 2;
    
    // Combat Power
    public float combatPower = 1f;
    
    // Abilities
    public List<string> abilities = new List<string>();
    public List<string> passiveSkills = new List<string>();
    
    // Equipment
    public Dictionary<string, string> equipment = new Dictionary<string, string>();
    
    public Sprite heroIcon;
    public string description;
}

public enum HeroClass
{
    Warrior,
    Mage,
    Archer,
    Support,
    Legendary
}

public static class HeroDataManager
{
    private static Dictionary<string, HeroData> heroDatabase;

    public static void Initialize()
    {
        heroDatabase = new Dictionary<string, HeroData>
        {
            // Warrior Class (10 heroes)
            { "barbarian_king", new HeroData { heroId = "barbarian_king", heroName = "Barbarian King", heroClass = HeroClass.Warrior, maxHealth = 200, attack = 25, defense = 10, speed = 7, description = "Legendary warrior with massive strength" } },
            { "knight", new HeroData { heroId = "knight", heroName = "Knight", heroClass = HeroClass.Warrior, maxHealth = 180, attack = 20, defense = 15, speed = 6, description = "Defensive warrior" } },
            { "samurai", new HeroData { heroId = "samurai", heroName = "Samurai", heroClass = HeroClass.Warrior, maxHealth = 160, attack = 28, defense = 8, speed = 10, description = "Fast and deadly" } },
            { "paladin", new HeroData { heroId = "paladin", heroName = "Paladin", heroClass = HeroClass.Warrior, maxHealth = 190, attack = 22, defense = 12, speed = 7, description = "Holy warrior" } },
            { "gladiator", new HeroData { heroId = "gladiator", heroName = "Gladiator", heroClass = HeroClass.Warrior, maxHealth = 170, attack = 26, defense = 9, speed = 9, description = "Arena fighter" } },
            { "warlord", new HeroData { heroId = "warlord", heroName = "Warlord", heroClass = HeroClass.Warrior, maxHealth = 210, attack = 24, defense = 11, speed = 6, description = "Supreme commander" } },
            { "berserker", new HeroData { heroId = "berserker", heroName = "Berserker", heroClass = HeroClass.Warrior, maxHealth = 180, attack = 30, defense = 6, speed = 11, description = "Rages into battle" } },
            { "crusader", new HeroData { heroId = "crusader", heroName = "Crusader", heroClass = HeroClass.Warrior, maxHealth = 185, attack = 23, defense = 13, speed = 7, description = "Holy crusader" } },
            { "viking", new HeroData { heroId = "viking", heroName = "Viking", heroClass = HeroClass.Warrior, maxHealth = 175, attack = 27, defense = 9, speed = 8, description = "Raider from the north" } },
            { "ronin", new HeroData { heroId = "ronin", heroName = "Ronin", heroClass = HeroClass.Warrior, maxHealth = 165, attack = 29, defense = 7, speed = 10, description = "Masterless swordsman" } },

            // Mage Class (7 heroes)
            { "merlin", new HeroData { heroId = "merlin", heroName = "Merlin", heroClass = HeroClass.Mage, maxHealth = 120, attack = 15, defense = 4, speed = 8, mana = 150, description = "Master of arcane magic" } },
            { "sorceress", new HeroData { heroId = "sorceress", heroName = "Sorceress", heroClass = HeroClass.Mage, maxHealth = 110, attack = 14, defense = 3, speed = 9, mana = 140, description = "Powerful mage" } },
            { "necromancer", new HeroData { heroId = "necromancer", heroName = "Necromancer", heroClass = HeroClass.Mage, maxHealth = 130, attack = 16, defense = 5, speed = 7, mana = 160, description = "Commands the undead" } },
            { "pyromancer", new HeroData { heroId = "pyromancer", heroName = "Pyromancer", heroClass = HeroClass.Mage, maxHealth = 115, attack = 18, defense = 4, speed = 8, mana = 145, description = "Fire master" } },
            { "frost_sage", new HeroData { heroId = "frost_sage", heroName = "Frost Sage", heroClass = HeroClass.Mage, maxHealth = 118, attack = 17, defense = 4, speed = 7, mana = 150, description = "Ice wielder" } },
            { "arch_mage", new HeroData { heroId = "arch_mage", heroName = "Arch Mage", heroClass = HeroClass.Mage, maxHealth = 125, attack = 19, defense = 5, speed = 8, mana = 170, description = "Eldest of mages" } },
            { "warlock", new HeroData { heroId = "warlock", heroName = "Warlock", heroClass = HeroClass.Mage, maxHealth = 122, attack = 17, defense = 4, speed = 8, mana = 155, description = "Dark magic specialist" } },

            // Archer Class (7 heroes)
            { "robin_hood", new HeroData { heroId = "robin_hood", heroName = "Robin Hood", heroClass = HeroClass.Archer, maxHealth = 140, attack = 22, defense = 6, speed = 12, description = "Master archer" } },
            { "artemis", new HeroData { heroId = "artemis", heroName = "Artemis", heroClass = HeroClass.Archer, maxHealth = 135, attack = 23, defense = 5, speed = 13, description = "Goddess of the hunt" } },
            { "sniper", new HeroData { heroId = "sniper", heroName = "Sniper", heroClass = HeroClass.Archer, maxHealth = 130, attack = 25, defense = 4, speed = 11, description = "Precision shooter" } },
            { "ranger", new HeroData { heroId = "ranger", heroName = "Ranger", heroClass = HeroClass.Archer, maxHealth = 138, attack = 21, defense = 6, speed = 11, description = "Nature's protector" } },
            { "outlaw", new HeroData { heroId = "outlaw", heroName = "Outlaw", heroClass = HeroClass.Archer, maxHealth = 132, attack = 24, defense = 5, speed = 12, description = "Lawless archer" } },
            { "huntress", new HeroData { heroId = "huntress", heroName = "Huntress", heroClass = HeroClass.Archer, maxHealth = 136, attack = 22, defense = 5, speed = 12, description = "Skilled hunter" } },
            { "marksman", new HeroData { heroId = "marksman", heroName = "Marksman", heroClass = HeroClass.Archer, maxHealth = 134, attack = 23, defense = 5, speed = 11, description = "Perfect aim" } },

            // Support Class (4 heroes)
            { "cleric", new HeroData { heroId = "cleric", heroName = "Cleric", heroClass = HeroClass.Support, maxHealth = 150, attack = 12, defense = 10, speed = 6, mana = 100, description = "Holy healer" } },
            { "druid", new HeroData { heroId = "druid", heroName = "Druid", heroClass = HeroClass.Support, maxHealth = 145, attack = 11, defense = 9, speed = 7, mana = 110, description = "Nature healer" } },
            { "bard", new HeroData { heroId = "bard", heroName = "Bard", heroClass = HeroClass.Support, maxHealth = 140, attack = 10, defense = 8, speed = 9, mana = 105, description = "Buff supporter" } },
            { "shaman", new HeroData { heroId = "shaman", heroName = "Shaman", heroClass = HeroClass.Support, maxHealth = 148, attack = 13, defense = 9, speed = 7, mana = 115, description = "Spiritual healer" } },

            // Legendary Class (2 heroes)
            { "dragon_lord", new HeroData { heroId = "dragon_lord", heroName = "Dragon Lord", heroClass = HeroClass.Legendary, maxHealth = 250, attack = 35, defense = 15, speed = 9, mana = 200, description = "Ultimate hero - Dragon Lord" } },
            { "phoenix_queen", new HeroData { heroId = "phoenix_queen", heroName = "Phoenix Queen", heroClass = HeroClass.Legendary, maxHealth = 240, attack = 32, defense = 14, speed = 10, mana = 190, description = "Ultimate hero - Phoenix Queen" } }
        };
    }

    public static HeroData GetHeroData(string heroId)
    {
        if (heroDatabase != null && heroDatabase.ContainsKey(heroId))
            return heroDatabase[heroId];
        return null;
    }

    public static Dictionary<string, HeroData> GetAllHeroes() => heroDatabase;
}
