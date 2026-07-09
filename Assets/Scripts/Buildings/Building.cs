using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{
    [SerializeField] private string buildingId;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private BuildingData buildingData;
    private Vector2Int gridPosition;
    private int currentLevel = 1;
    private float buildProgress = 0f;
    private bool isBuilt = false;
    private float resourceGenerationTimer = 0f;

    public string BuildingId => buildingId;
    public Vector2Int GridPosition => gridPosition;
    public int CurrentLevel => currentLevel;
    public bool IsBuilt => isBuilt;

    public void Initialize(string id, Vector2Int pos)
    {
        buildingId = id;
        gridPosition = pos;
        buildingData = BuildingDataManager.GetBuildingData(buildingId);

        if (buildingData != null)
        {
            GetComponent<SpriteRenderer>().sprite = buildingData.icon;
            StartCoroutine(BuildBuilding());
        }
    }

    private IEnumerator BuildBuilding()
    {
        float elapsedTime = 0f;
        isBuilt = false;

        while (elapsedTime < buildingData.buildTime)
        {
            elapsedTime += Time.deltaTime;
            buildProgress = elapsedTime / buildingData.buildTime;
            yield return null;
        }

        buildProgress = 1f;
        isBuilt = true;
        Debug.Log($"{buildingData.buildingName} built at {gridPosition}");
    }

    private void Update()
    {
        if (isBuilt && buildingData.goldPerMinute > 0)
        {
            resourceGenerationTimer += Time.deltaTime;
            if (resourceGenerationTimer >= 60f)
            {
                GenerateResources();
                resourceGenerationTimer = 0f;
            }
        }
    }

    private void GenerateResources()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResourceManager.AddGold(buildingData.goldPerMinute * currentLevel);
            GameManager.Instance.ResourceManager.AddElixir(buildingData.elixirPerMinute * currentLevel);
        }
    }

    public void Upgrade()
    {
        if (currentLevel < buildingData.maxLevel)
        {
            currentLevel++;
            Debug.Log($"{buildingData.buildingName} upgraded to level {currentLevel}");
        }
    }

    public void Destroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GridSystem.RemoveBuilding(gridPosition, buildingData.width, buildingData.height);
        }
        Destroy(gameObject);
    }

    public float GetBuildProgress() => buildProgress;
    public BuildingData GetBuildingData() => buildingData;
}
