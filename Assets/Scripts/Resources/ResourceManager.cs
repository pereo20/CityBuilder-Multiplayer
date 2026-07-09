using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int startingGold = 1000;
    [SerializeField] private int startingElixir = 500;

    private int currentGold;
    private int currentElixir;
    private int currentGems = 0;
    private int currentDarkElixir = 0;

    public event Action<int> OnGoldChanged;
    public event Action<int> OnElixirChanged;
    public event Action<int> OnGemsChanged;
    public event Action<int> OnDarkElixirChanged;

    public void Initialize()
    {
        currentGold = startingGold;
        currentElixir = startingElixir;
        Debug.Log($"Resources initialized: Gold={currentGold}, Elixir={currentElixir}");
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        OnGoldChanged?.Invoke(currentGold);
    }

    public void RemoveGold(int amount)
    {
        currentGold = Mathf.Max(0, currentGold - amount);
        OnGoldChanged?.Invoke(currentGold);
    }

    public void AddElixir(int amount)
    {
        currentElixir += amount;
        OnElixirChanged?.Invoke(currentElixir);
    }

    public void RemoveElixir(int amount)
    {
        currentElixir = Mathf.Max(0, currentElixir - amount);
        OnElixirChanged?.Invoke(currentElixir);
    }

    public void AddGems(int amount)
    {
        currentGems += amount;
        OnGemsChanged?.Invoke(currentGems);
    }

    public void AddDarkElixir(int amount)
    {
        currentDarkElixir += amount;
        OnDarkElixirChanged?.Invoke(currentDarkElixir);
    }

    public int GetGold() => currentGold;
    public int GetElixir() => currentElixir;
    public int GetGems() => currentGems;
    public int GetDarkElixir() => currentDarkElixir;
}
