using Firebase;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FirebaseManager : MonoBehaviour
{
    private FirebaseAuth auth;
    private DatabaseReference database;
    private FirebaseUser user;
    private string playerId;

    public async Task Initialize()
    {
        try
        {
            // Initialize Firebase
            var task = FirebaseApp.CheckAndFixDependenciesAsync();
            await task;

            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                database = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase initialized successfully");
                
                // Anonymous authentication
                await auth.SignInAnonymouslyAsync();
                user = auth.CurrentUser;
                playerId = user.UserId;
                Debug.Log($"Logged in as: {playerId}");
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Firebase initialization error: {ex.Message}");
        }
    }

    public void SavePlayerData(string playerName, int level, int gold)
    {
        if (database == null) return;

        var playerData = new Dictionary<string, object>
        {
            { "name", playerName },
            { "level", level },
            { "gold", gold },
            { "lastUpdated", ServerTimestamp.Timestamp.GetCurrentTimestamp() }
        };

        database.Child("players").Child(playerId).SetValueAsync(playerData);
    }

    public void SaveHeroData(string heroId, int level, int attack, int defense)
    {
        if (database == null) return;

        var heroData = new Dictionary<string, object>
        {
            { "level", level },
            { "attack", attack },
            { "defense", defense },
            { "lastUpdated", ServerTimestamp.Timestamp.GetCurrentTimestamp() }
        };

        database.Child("players").Child(playerId).Child("heroes").Child(heroId).SetValueAsync(heroData);
    }

    public string GetPlayerId() => playerId;
}
