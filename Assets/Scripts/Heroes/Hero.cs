using UnityEngine;
using System.Collections.Generic;

public class Hero : MonoBehaviour
{
    private HeroData heroData;
    private int currentExperience = 0;
    private bool isInBattle = false;

    public HeroData HeroData => heroData;
    public int CurrentExperience => currentExperience;
    public bool IsInBattle => isInBattle;

    public void Initialize(string heroId)
    {
        HeroData baseData = HeroDataManager.GetHeroData(heroId);
        if (baseData != null)
        {
            // Create a copy of the data
            heroData = new HeroData
            {
                heroId = baseData.heroId,
                heroName = baseData.heroName,
                heroClass = baseData.heroClass,
                level = 1,
                maxLevel = baseData.maxLevel,
                experience = 0,
                experienceToNextLevel = 100,
                maxHealth = baseData.maxHealth,
                currentHealth = baseData.maxHealth,
                attack = baseData.attack,
                defense = baseData.defense,
                speed = baseData.speed,
                mana = baseData.mana,
                manaRegeneration = baseData.manaRegeneration,
                combatPower = CalculateCombatPower(baseData),
                heroIcon = baseData.heroIcon,
                description = baseData.description
            };
        }
    }

    public void GainExperience(int amount)
    {
        currentExperience += amount;
        if (currentExperience >= heroData.experienceToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        if (heroData.level < heroData.maxLevel)
        {
            heroData.level++;
            currentExperience = 0;
            heroData.experienceToNextLevel = Mathf.RoundToInt(heroData.experienceToNextLevel * 1.1f);

            // Boost stats
            heroData.maxHealth = Mathf.RoundToInt(heroData.maxHealth * 1.05f);
            heroData.currentHealth = heroData.maxHealth;
            heroData.attack = Mathf.RoundToInt(heroData.attack * 1.08f);
            heroData.defense = Mathf.RoundToInt(heroData.defense * 1.05f);
            heroData.mana = Mathf.RoundToInt(heroData.mana * 1.05f);
            heroData.combatPower = CalculateCombatPower(heroData);

            Debug.Log($"{heroData.heroName} leveled up to {heroData.level}!");
        }
    }

    public void UpgradeAttack()
    {
        heroData.attack = Mathf.RoundToInt(heroData.attack * 1.1f);
        heroData.combatPower = CalculateCombatPower(heroData);
        Debug.Log($"{heroData.heroName} attack increased to {heroData.attack}");
    }

    public void UpgradeDefense()
    {
        heroData.defense = Mathf.RoundToInt(heroData.defense * 1.1f);
        heroData.combatPower = CalculateCombatPower(heroData);
        Debug.Log($"{heroData.heroName} defense increased to {heroData.defense}");
    }

    public void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(1, damage - heroData.defense / 2);
        heroData.currentHealth -= actualDamage;
        if (heroData.currentHealth < 0)
            heroData.currentHealth = 0;
    }

    public void Heal(int amount)
    {
        heroData.currentHealth = Mathf.Min(heroData.maxHealth, heroData.currentHealth + amount);
    }

    public float GetCombatPower() => heroData.combatPower;

    private float CalculateCombatPower(HeroData data)
    {
        return (data.attack * 2f + data.defense + data.level * 1.5f) / 10f;
    }

    public void SetInBattle(bool inBattle)
    {
        isInBattle = inBattle;
    }
}
