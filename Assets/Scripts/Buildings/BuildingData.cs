using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BuildingData
{
    public string buildingId;
    public string buildingName;
    public BuildingType buildingType;
    public int width = 1;
    public int height = 1;
    public int goldCost = 100;
    public int elixirCost = 0;
    public float buildTime = 5f;
    public int maxLevel = 10;
    public Sprite icon;
    public string description;

    // Resource generation
    public int goldPerMinute = 0;
    public int elixirPerMinute = 0;

    // Training
    public List<string> trainableUnits = new List<string>();
    public int trainingCapacity = 10;

    // Defense
    public int defense = 0;
    public int attackPower = 0;
    public float attackRange = 5f;
    public float attackSpeed = 1f;
}

public enum BuildingType
{
    Resource,
    Defense,
    Training,
    Storage,
    Support,
    Special
}

public static class BuildingDataManager
{
    private static Dictionary<string, BuildingData> buildingDatabase;

    public static void Initialize()
    {
        buildingDatabase = new Dictionary<string, BuildingData>
        {
            // Resource Buildings
            { "gold_mine", new BuildingData { buildingId = "gold_mine", buildingName = "Gold Mine", buildingType = BuildingType.Resource, goldCost = 100, buildTime = 10f, goldPerMinute = 5, description = "Generates gold over time" } },
            { "elixir_collector", new BuildingData { buildingId = "elixir_collector", buildingName = "Elixir Collector", buildingType = BuildingType.Resource, goldCost = 150, buildTime = 15f, elixirPerMinute = 3, description = "Generates elixir" } },
            { "dark_elixir_drill", new BuildingData { buildingId = "dark_elixir_drill", buildingName = "Dark Elixir Drill", buildingType = BuildingType.Resource, goldCost = 250, buildTime = 20f, description = "Generates dark elixir" } },
            { "gem_mine", new BuildingData { buildingId = "gem_mine", buildingName = "Gem Mine", buildingType = BuildingType.Resource, goldCost = 500, buildTime = 30f, description = "Generates gems" } },

            // Defense Buildings
            { "cannon", new BuildingData { buildingId = "cannon", buildingName = "Cannon", buildingType = BuildingType.Defense, goldCost = 200, buildTime = 10f, defense = 5, attackPower = 8, attackRange = 8f, description = "Defensive tower" } },
            { "archer_tower", new BuildingData { buildingId = "archer_tower", buildingName = "Archer Tower", buildingType = BuildingType.Defense, goldCost = 180, buildTime = 8f, defense = 3, attackPower = 6, attackRange = 10f, description = "Long range defense" } },
            { "wizard_tower", new BuildingData { buildingId = "wizard_tower", buildingName = "Wizard Tower", buildingType = BuildingType.Defense, goldCost = 300, buildTime = 15f, defense = 4, attackPower = 10, attackRange = 12f, description = "Magical defense" } },
            { "inferno_tower", new BuildingData { buildingId = "inferno_tower", buildingName = "Inferno Tower", buildingType = BuildingType.Defense, goldCost = 400, buildTime = 20f, defense = 6, attackPower = 15, attackRange = 9f, description = "Devastating fire attacks" } },
            { "bomb_tower", new BuildingData { buildingId = "bomb_tower", buildingName = "Bomb Tower", buildingType = BuildingType.Defense, goldCost = 220, buildTime = 12f, defense = 4, attackPower = 12, attackRange = 7f, description = "Explosive defense" } },
            { "tesla_tower", new BuildingData { buildingId = "tesla_tower", buildingName = "Tesla Tower", buildingType = BuildingType.Defense, goldCost = 350, buildTime = 18f, defense = 5, attackPower = 11, attackRange = 8f, description = "Electric defense" } },

            // Training Facilities
            { "barracks", new BuildingData { buildingId = "barracks", buildingName = "Barracks", buildingType = BuildingType.Training, goldCost = 150, buildTime = 10f, trainingCapacity = 20, description = "Train troops" } },
            { "dark_barracks", new BuildingData { buildingId = "dark_barracks", buildingName = "Dark Barracks", buildingType = BuildingType.Training, goldCost = 250, buildTime = 15f, trainingCapacity = 15, description = "Train dark troops" } },
            { "dragon_roost", new BuildingData { buildingId = "dragon_roost", buildingName = "Dragon Roost", buildingType = BuildingType.Training, goldCost = 400, buildTime = 20f, trainingCapacity = 5, description = "Train dragons" } },
            { "spell_factory", new BuildingData { buildingId = "spell_factory", buildingName = "Spell Factory", buildingType = BuildingType.Training, goldCost = 300, buildTime = 15f, description = "Create spells" } },
            { "academy", new BuildingData { buildingId = "academy", buildingName = "Academy", buildingType = BuildingType.Training, goldCost = 350, buildTime = 18f, description = "Train heroes" } },

            // Storage Buildings
            { "gold_storage", new BuildingData { buildingId = "gold_storage", buildingName = "Gold Storage", buildingType = BuildingType.Storage, goldCost = 100, buildTime = 8f, description = "Store gold" } },
            { "elixir_storage", new BuildingData { buildingId = "elixir_storage", buildingName = "Elixir Storage", buildingType = BuildingType.Storage, goldCost = 120, buildTime = 8f, description = "Store elixir" } },
            { "dark_elixir_storage", new BuildingData { buildingId = "dark_elixir_storage", buildingName = "Dark Elixir Storage", buildingType = BuildingType.Storage, goldCost = 200, buildTime = 12f, description = "Store dark elixir" } },

            // Support Buildings
            { "town_hall", new BuildingData { buildingId = "town_hall", buildingName = "Town Hall", buildingType = BuildingType.Special, goldCost = 0, buildTime = 0f, description = "Your main building" } },
            { "wall", new BuildingData { buildingId = "wall", buildingName = "Wall", buildingType = BuildingType.Defense, goldCost = 50, buildTime = 2f, defense = 2, description = "Defensive wall" } },
            { "hospital", new BuildingData { buildingId = "hospital", buildingName = "Hospital", buildingType = BuildingType.Support, goldCost = 200, buildTime = 10f, description = "Heal troops" } },
            { "lab", new BuildingData { buildingId = "lab", buildingName = "Laboratory", buildingType = BuildingType.Support, goldCost = 250, buildTime = 15f, description = "Upgrade troops" } },
            { "altar", new BuildingData { buildingId = "altar", buildingName = "Altar", buildingType = BuildingType.Special, goldCost = 300, buildTime = 15f, description = "Summon special units" } },
            { "monument", new BuildingData { buildingId = "monument", buildingName = "Monument", buildingType = BuildingType.Special, goldCost = 500, buildTime = 25f, description = "Increase production" } },
            { "forge", new BuildingData { buildingId = "forge", buildingName = "Forge", buildingType = BuildingType.Support, goldCost = 200, buildTime = 12f, description = "Craft equipment" } },
            { "tower_of_light", new BuildingData { buildingId = "tower_of_light", buildingName = "Tower of Light", buildingType = BuildingType.Special, goldCost = 600, buildTime = 30f, description = "Special defensive building" } },
            { "portal", new BuildingData { buildingId = "portal", buildingName = "Portal", buildingType = BuildingType.Special, goldCost = 400, buildTime = 20f, description = "Teleport units" } },
            { "observatory", new BuildingData { buildingId = "observatory", buildingName = "Observatory", buildingType = BuildingType.Support, goldCost = 350, buildTime = 18f, description = "Scout enemy cities" } }
        };
    }

    public static BuildingData GetBuildingData(string buildingId)
    {
        if (buildingDatabase.ContainsKey(buildingId))
            return buildingDatabase[buildingId];
        return null;
    }

    public static Dictionary<string, BuildingData> GetAllBuildings() => buildingDatabase;
}
