using UnityEngine;
using System.Collections.Generic;

public class CombatCalculator
{
    public static int CalculateBattleResult(List<Hero> attackingHeroes, List<Hero> defendingHeroes)
    {
        float attackPower = 0f;
        float defensePower = 0f;

        foreach (Hero hero in attackingHeroes)
        {
            attackPower += (hero.HeroData.attack + hero.HeroData.level) * 1.2f;
        }

        foreach (Hero hero in defendingHeroes)
        {
            defensePower += (hero.HeroData.defense + hero.HeroData.level) * 1.2f;
        }

        // Add random factor
        float randomFactor = Random.Range(0.8f, 1.2f);
        attackPower *= randomFactor;

        // Determine winner
        if (attackPower > defensePower)
            return 1; // Attacker wins
        else if (defensePower > attackPower)
            return -1; // Defender wins
        else
            return 0; // Draw
    }

    public static int CalculateDamage(Hero attacker, Hero defender)
    {
        int baseDamage = attacker.HeroData.attack + (attacker.HeroData.level * 2);
        float variance = Random.Range(0.8f, 1.2f);
        int finalDamage = Mathf.RoundToInt(baseDamage * variance);
        return finalDamage;
    }

    public static int CalculateGoldReward(int enemyDefense, int battleDifficulty)
    {
        return Mathf.RoundToInt(enemyDefense * 10 * (1f + battleDifficulty * 0.5f));
    }

    public static int CalculateExperienceReward(int enemyLevel, int heroLevel)
    {
        int baseExp = 50;
        float levelDifference = Mathf.Max(0, enemyLevel - heroLevel);
        return Mathf.RoundToInt(baseExp * (1f + levelDifference * 0.1f));
    }
}
