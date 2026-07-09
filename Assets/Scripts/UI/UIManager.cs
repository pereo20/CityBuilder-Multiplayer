using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject resourceDisplayPrefab;
    [SerializeField] private GameObject heroListPrefab;
    [SerializeField] private GameObject buildingMenuPrefab;

    private ResourceManager resourceManager;
    private HeroManager heroManager;
    private BuildingSystem buildingSystem;

    public void Initialize()
    {
        resourceManager = GameManager.Instance.ResourceManager;
        heroManager = GameManager.Instance.HeroManager;
        buildingSystem = GameManager.Instance.BuildingSystem;

        // Subscribe to resource changes
        resourceManager.OnGoldChanged += UpdateGoldDisplay;
        resourceManager.OnElixirChanged += UpdateElixirDisplay;

        Debug.Log("UIManager initialized");
    }

    private void UpdateGoldDisplay(int gold)
    {
        // Update UI element
        Debug.Log($"Gold: {gold}");
    }

    private void UpdateElixirDisplay(int elixir)
    {
        // Update UI element
        Debug.Log($"Elixir: {elixir}");
    }

    public void ShowHeroList()
    {
        Debug.Log("Showing hero list");
    }

    public void ShowBuildingMenu()
    {
        Debug.Log("Showing building menu");
    }

    public void ShowBattleScreen()
    {
        Debug.Log("Showing battle screen");
    }
}
