using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private BuildingSystem buildingSystem;
    [SerializeField] private HeroManager heroManager;
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private FirebaseManager firebaseManager;
    [SerializeField] private UIManager uiManager;

    private DatabaseReference databaseReference;
    private string playerId;
    private bool isInitialized = false;

    public GridSystem GridSystem => gridSystem;
    public BuildingSystem BuildingSystem => buildingSystem;
    public HeroManager HeroManager => heroManager;
    public ResourceManager ResourceManager => resourceManager;
    public FirebaseManager FirebaseManager => firebaseManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializeGame();
    }

    private async void InitializeGame()
    {
        Debug.Log("Initializing CityBuilder Multiplayer Game...");

        // Initialize Firebase
        await firebaseManager.Initialize();
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // Generate or load player ID
        playerId = PlayerPrefs.GetString("PlayerID", System.Guid.NewGuid().ToString());
        PlayerPrefs.SetString("PlayerID", playerId);

        // Initialize systems
        gridSystem.Initialize(50, 50);
        buildingSystem.Initialize();
        heroManager.Initialize();
        resourceManager.Initialize();
        uiManager.Initialize();

        isInitialized = true;
        Debug.Log("Game initialized! Player ID: " + playerId);
    }

    public string GetPlayerId() => playerId;

    public bool IsInitialized() => isInitialized;

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
