using UnityEngine;
using System.Collections.Generic;

public class HeroManager : MonoBehaviour
{
    [SerializeField] private Transform heroContainer;
    [SerializeField] private GameObject heroPrefab;

    private Dictionary<string, Hero> ownedHeroes = new Dictionary<string, Hero>();
    private List<Hero> activeHeroes = new List<Hero>(); // Heroes selected for battle

    public void Initialize()
    {
        HeroDataManager.Initialize();
        
        if (heroContainer == null)
        {
            heroContainer = new GameObject("Heroes").transform;
        }
    }

    public Hero RecruitHero(string heroId)
    {
        if (ownedHeroes.ContainsKey(heroId))
        {
            Debug.LogWarning($"Hero {heroId} already recruited");
            return ownedHeroes[heroId];
        }

        HeroData heroData = HeroDataManager.GetHeroData(heroId);
        if (heroData == null)
        {
            Debug.LogError($"Hero data not found for {heroId}");
            return null;
        }

        GameObject heroGO = Instantiate(heroPrefab, heroContainer);
        Hero hero = heroGO.AddComponent<Hero>();
        hero.Initialize(heroId);

        ownedHeroes[heroId] = hero;
        Debug.Log($"Hero {heroData.heroName} recruited!");
        return hero;
    }

    public Hero GetHero(string heroId)
    {
        if (ownedHeroes.ContainsKey(heroId))
            return ownedHeroes[heroId];
        return null;
    }

    public void SelectHeroForBattle(string heroId)
    {
        Hero hero = GetHero(heroId);
        if (hero != null && !activeHeroes.Contains(hero))
        {
            activeHeroes.Add(hero);
            hero.SetInBattle(true);
            Debug.Log($"{hero.HeroData.heroName} selected for battle");
        }
    }

    public void DeselectHeroFromBattle(string heroId)
    {
        Hero hero = GetHero(heroId);
        if (hero != null && activeHeroes.Contains(hero))
        {
            activeHeroes.Remove(hero);
            hero.SetInBattle(false);
            Debug.Log($"{hero.HeroData.heroName} deselected from battle");
        }
    }

    public List<Hero> GetActiveBattleHeroes() => new List<Hero>(activeHeroes);
    public Dictionary<string, Hero> GetOwnedHeroes() => ownedHeroes;
    public int GetTotalCombatPower()
    {
        int totalPower = 0;
        foreach (var hero in activeHeroes)
        {
            totalPower += Mathf.RoundToInt(hero.GetCombatPower());
        }
        return totalPower;
    }
}
