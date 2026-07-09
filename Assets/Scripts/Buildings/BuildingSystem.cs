using UnityEngine;
using System.Collections.Generic;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private Transform buildingContainer;
    [SerializeField] private GameObject buildingPrefab;

    private Dictionary<Vector2Int, Building> buildings = new Dictionary<Vector2Int, Building>();
    private GridSystem gridSystem;

    public void Initialize()
    {
        gridSystem = GameManager.Instance.GridSystem;
        BuildingDataManager.Initialize();
        
        if (buildingContainer == null)
        {
            buildingContainer = new GameObject("Buildings").transform;
        }
    }

    public bool TryPlaceBuilding(string buildingId, Vector2Int gridPos)
    {
        BuildingData data = BuildingDataManager.GetBuildingData(buildingId);
        if (data == null)
        {
            Debug.LogError($"Building data not found for {buildingId}");
            return false;
        }

        // Check resources
        ResourceManager resourceMgr = GameManager.Instance.ResourceManager;
        if (resourceMgr.GetGold() < data.goldCost || 
            resourceMgr.GetElixir() < data.elixirCost)
        {
            Debug.LogError("Not enough resources");
            return false;
        }

        // Check grid space
        if (!gridSystem.CanPlaceBuilding(gridPos, data.width, data.height))
        {
            Debug.LogError("Cannot place building here");
            return false;
        }

        // Deduct resources
        resourceMgr.RemoveGold(data.goldCost);
        resourceMgr.RemoveElixir(data.elixirCost);

        // Create building
        GameObject buildingGO = Instantiate(buildingPrefab, buildingContainer);
        Building building = buildingGO.AddComponent<Building>();
        building.Initialize(buildingId, gridPos);

        Vector3 worldPos = gridSystem.GetWorldPosition(gridPos);
        buildingGO.transform.position = worldPos;

        gridSystem.PlaceBuilding(gridPos, data.width, data.height, building);
        buildings[gridPos] = building;

        Debug.Log($"Building {buildingId} placed at {gridPos}");
        return true;
    }

    public Building GetBuilding(Vector2Int gridPos)
    {
        if (buildings.ContainsKey(gridPos))
            return buildings[gridPos];
        return null;
    }

    public void RemoveBuilding(Vector2Int gridPos)
    {
        if (buildings.ContainsKey(gridPos))
        {
            buildings[gridPos].Destroy();
            buildings.Remove(gridPos);
        }
    }

    public Dictionary<Vector2Int, Building> GetAllBuildings() => buildings;
}
