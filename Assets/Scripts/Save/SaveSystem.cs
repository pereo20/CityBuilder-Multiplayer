using UnityEngine;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour
{
    private const string SAVE_KEY_PREFIX = "CityBuilder_";

    public static void SaveGameData(string playerId, int level, int gold, int elixir)
    {
        PlayerPrefs.SetString(SAVE_KEY_PREFIX + "PlayerID", playerId);
        PlayerPrefs.SetInt(SAVE_KEY_PREFIX + "Level", level);
        PlayerPrefs.SetInt(SAVE_KEY_PREFIX + "Gold", gold);
        PlayerPrefs.SetInt(SAVE_KEY_PREFIX + "Elixir", elixir);
        PlayerPrefs.Save();
    }

    public static void SaveHeroData(string heroId, int level, int experience, int attack, int defense)
    {
        string key = SAVE_KEY_PREFIX + "Hero_" + heroId;
        PlayerPrefs.SetInt(key + "_Level", level);
        PlayerPrefs.SetInt(key + "_Experience", experience);
        PlayerPrefs.SetInt(key + "_Attack", attack);
        PlayerPrefs.SetInt(key + "_Defense", defense);
        PlayerPrefs.Save();
    }

    public static void SaveBuildingData(string buildingId, Vector2Int position, int level)
    {
        string key = SAVE_KEY_PREFIX + "Building_" + buildingId + "_" + position.ToString();
        PlayerPrefs.SetInt(key + "_Level", level);
        PlayerPrefs.Save();
    }

    public static Dictionary<string, string> LoadGameData()
    {
        var data = new Dictionary<string, string>
        {
            { "PlayerID", PlayerPrefs.GetString(SAVE_KEY_PREFIX + "PlayerID", "") },
            { "Level", PlayerPrefs.GetInt(SAVE_KEY_PREFIX + "Level", 1).ToString() },
            { "Gold", PlayerPrefs.GetInt(SAVE_KEY_PREFIX + "Gold", 1000).ToString() },
            { "Elixir", PlayerPrefs.GetInt(SAVE_KEY_PREFIX + "Elixir", 500).ToString() }
        };
        return data;
    }

    public static bool HasSavedGame()
    {
        return PlayerPrefs.HasKey(SAVE_KEY_PREFIX + "PlayerID");
    }

    public static void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();
    }
}