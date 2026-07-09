using UnityEngine;
using System.Collections.Generic;

public class GridSystem : MonoBehaviour
{
    private int gridWidth;
    private int gridHeight;
    private float cellSize = 1f;
    private Grid<GridCell> grid;

    [System.Serializable]
    public class GridCell
    {
        public bool isOccupied = false;
        public Building occupyingBuilding = null;
        public Vector2Int gridPosition;
    }

    public void Initialize(int width, int height)
    {
        gridWidth = width;
        gridHeight = height;
        grid = new Grid<GridCell>(gridWidth, gridHeight, cellSize, Vector3.zero);

        // Initialize all cells
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GridCell cell = new GridCell { gridPosition = new Vector2Int(x, y) };
                grid.SetGridObject(x, y, cell);
            }
        }

        Debug.Log($"Grid initialized: {gridWidth}x{gridHeight}");
    }

    public bool CanPlaceBuilding(Vector2Int gridPos, int width, int height)
    {
        if (gridPos.x < 0 || gridPos.y < 0 || 
            gridPos.x + width > gridWidth || gridPos.y + height > gridHeight)
        {
            return false;
        }

        for (int x = gridPos.x; x < gridPos.x + width; x++)
        {
            for (int y = gridPos.y; y < gridPos.y + height; y++)
            {
                if (grid.GetGridObject(x, y).isOccupied)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void PlaceBuilding(Vector2Int gridPos, int width, int height, Building building)
    {
        if (CanPlaceBuilding(gridPos, width, height))
        {
            for (int x = gridPos.x; x < gridPos.x + width; x++)
            {
                for (int y = gridPos.y; y < gridPos.y + height; y++)
                {
                    GridCell cell = grid.GetGridObject(x, y);
                    cell.isOccupied = true;
                    cell.occupyingBuilding = building;
                }
            }
        }
    }

    public void RemoveBuilding(Vector2Int gridPos, int width, int height)
    {
        for (int x = gridPos.x; x < gridPos.x + width; x++)
        {
            for (int y = gridPos.y; y < gridPos.y + height; y++)
            {
                if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
                {
                    GridCell cell = grid.GetGridObject(x, y);
                    cell.isOccupied = false;
                    cell.occupyingBuilding = null;
                }
            }
        }
    }

    public Vector3 GetWorldPosition(Vector2Int gridPos)
    {
        return grid.GetWorldPosition(gridPos.x, gridPos.y);
    }

    public Vector2Int GetGridPosition(Vector3 worldPos)
    {
        grid.GetXY(worldPos, out int x, out int y);
        return new Vector2Int(x, y);
    }

    public int GetGridWidth() => gridWidth;
    public int GetGridHeight() => gridHeight;
}

public class Grid<TGridObject>
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new TGridObject[width, height];
    }

    public Vector3 GetWorldPosition(int x, int y) => 
        new Vector3(x, 0, y) * cellSize + originPosition;

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }

    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
            gridArray[x, y] = value;
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
            return gridArray[x, y];
        return default;
    }
}
