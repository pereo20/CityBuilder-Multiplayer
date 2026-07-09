using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PlayerManager : MonoBehaviour
{
    private DatabaseReference database;
    private string playerId;
    private string playerName;
    private int playerLevel = 1;
    private int playerTownHallLevel = 1;

    public event System.Action OnPlayerDataUpdated;

    public void Initialize(string id, string name)
    {
        playerId = id;
        playerName = name;
        database = FirebaseDatabase.DefaultInstance.RootReference;
        LoadPlayerData();
    }

    public async void LoadPlayerData()
    {
        try
        {
            var snapshot = await database.Child("players").Child(playerId).GetValueAsync();
            if (snapshot.Exists)
            {
                var playerData = snapshot.Value as Dictionary<object, object>;
                if (playerData != null)
                {
                    if (playerData.TryGetValue("level", out var level))
                        playerLevel = int.Parse(level.ToString());
                    if (playerData.TryGetValue("townHallLevel", out var townHall))
                        playerTownHallLevel = int.Parse(townHall.ToString());
                }
            }
            else
            {
                var newPlayerData = new Dictionary<string, object>
                {
                    { "name", playerName },
                    { "level", 1 },
                    { "townHallLevel", 1 },
                    { "gold", 1000 },
                    { "elixir", 500 },
                    { "createdAt", ServerTimestamp.Timestamp.GetCurrentTimestamp() }
                };
                await database.Child("players").Child(playerId).SetValueAsync(newPlayerData);
            }
            OnPlayerDataUpdated?.Invoke();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error loading player data: {ex.Message}");
        }
    }

    public async void UpdatePlayerLevel(int newLevel)
    {
        playerLevel = newLevel;
        await database.Child("players").Child(playerId).Child("level").SetValueAsync(newLevel);
    }

    public async void UpdateTownHallLevel(int newLevel)
    {
        playerTownHallLevel = newLevel;
        await database.Child("players").Child(playerId).Child("townHallLevel").SetValueAsync(newLevel);
    }

    public string GetPlayerId() => playerId;
    public string GetPlayerName() => playerName;
    public int GetPlayerLevel() => playerLevel;
    public int GetTownHallLevel() => playerTownHallLevel;
}