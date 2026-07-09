using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RaidSystem : MonoBehaviour
{
    private DatabaseReference database;
    private string playerId;
    private CombatSystem combatSystem;

    [System.Serializable]
    public class RaidData
    {
        public string raidId;
        public string attackerId;
        public string attackerName;
        public string defenderId;
        public string defenderName;
        public int goldStolen = 0;
        public int elixirStolen = 0;
        public bool successful = false;
        public string timestamp;
    }

    public event System.Action<RaidData> OnRaidReceived;

    public void Initialize(string id, CombatSystem combat)
    {
        playerId = id;
        combatSystem = combat;
        database = FirebaseDatabase.DefaultInstance.RootReference;
        ListenForRaids();
    }

    public async void InitiateRaid(string targetPlayerId, List<Hero> attackingHeroes, string defenderName, int goldInStorage, int elixirInStorage)
    {
        if (combatSystem.IsInCombat()) return;

        int goldStealable = Mathf.RoundToInt(goldInStorage * 0.3f);
        int elixirStealable = Mathf.RoundToInt(elixirInStorage * 0.3f);

        int battleResult = CombatCalculator.CalculateBattleResult(attackingHeroes, new List<Hero>());
        bool raidSuccessful = battleResult > 0;
        
        if (raidSuccessful)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ResourceManager.AddGold(goldStealable);
                GameManager.Instance.ResourceManager.AddElixir(elixirStealable);
            }
        }

        var raidData = new RaidData
        {
            raidId = System.Guid.NewGuid().ToString(),
            attackerId = playerId,
            attackerName = "Player",
            defenderId = targetPlayerId,
            defenderName = defenderName,
            goldStolen = raidSuccessful ? goldStealable : 0,
            elixirStolen = raidSuccessful ? elixirStealable : 0,
            successful = raidSuccessful,
            timestamp = System.DateTime.Now.ToString()
        };

        await SaveRaidToDatabase(raidData);
        Debug.Log($"Raid {(raidSuccessful ? "successful" : "failed")}!");
    }

    private async Task SaveRaidToDatabase(RaidData raid)
    {
        try
        {
            var raidDict = new Dictionary<string, object>
            {
                { "raidId", raid.raidId },
                { "attackerId", raid.attackerId },
                { "attackerName", raid.attackerName },
                { "defenderId", raid.defenderId },
                { "defenderName", raid.defenderName },
                { "goldStolen", raid.goldStolen },
                { "elixirStolen", raid.elixirStolen },
                { "successful", raid.successful },
                { "timestamp", raid.timestamp }
            };

            await database.Child("raids").Child(raid.raidId).SetValueAsync(raidDict);
            await database.Child("players").Child(raid.defenderId).Child("raidHistory").Child(raid.raidId).SetValueAsync(true);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error saving raid: {ex.Message}");
        }
    }

    private void ListenForRaids()
    {
        database.Child("players").Child(playerId).Child("raidHistory").OnChildAdded += HandleRaidReceived;
    }

    private void HandleRaidReceived(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError($"Database error: {args.DatabaseError.Message}");
            return;
        }
        Debug.Log("New raid received!");
    }

    public async void GetPlayerCityData(string targetPlayerId, System.Action<Dictionary<string, object>> callback)
    {
        try
        {
            var snapshot = await database.Child("players").Child(targetPlayerId).GetValueAsync();
            if (snapshot.Exists)
            {
                var cityData = snapshot.Value as Dictionary<string, object>;
                callback?.Invoke(cityData);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error getting city data: {ex.Message}");
        }
    }
}