using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private float combatDuration = 5f;
    private bool isInCombat = false;

    public event System.Action<int> OnBattleResult; // 1 = win, -1 = loss, 0 = draw

    public void InitiateBattle(List<Hero> attackingHeroes, List<Hero> defendingHeroes)
    {
        if (isInCombat) return;

        StartCoroutine(CombatRoutine(attackingHeroes, defendingHeroes));
    }

    private IEnumerator CombatRoutine(List<Hero> attackingHeroes, List<Hero> defendingHeroes)
    {
        isInCombat = true;
        
        Debug.Log("Battle started!");
        foreach (Hero hero in attackingHeroes)
            hero.SetInBattle(true);
        foreach (Hero hero in defendingHeroes)
            hero.SetInBattle(true);

        // Simulate combat duration
        yield return new WaitForSeconds(combatDuration);

        // Calculate battle result
        int result = CombatCalculator.CalculateBattleResult(attackingHeroes, defendingHeroes);

        if (result == 1)
        {
            Debug.Log("Attacker wins!");
            RewardAttackers(attackingHeroes, defendingHeroes);
        }
        else if (result == -1)
        {
            Debug.Log("Defender wins!");
        }
        else
        {
            Debug.Log("Draw!");
        }

        OnBattleResult?.Invoke(result);

        // End combat
        foreach (Hero hero in attackingHeroes)
            hero.SetInBattle(false);
        foreach (Hero hero in defendingHeroes)
            hero.SetInBattle(false);

        isInCombat = false;
    }

    private void RewardAttackers(List<Hero> attackers, List<Hero> defenders)
    {
        int totalDefenderLevel = 0;
        foreach (Hero defender in defenders)
            totalDefenderLevel += defender.HeroData.level;

        int avgDefenderLevel = totalDefenderLevel / defenders.Count;
        int gold = CombatCalculator.CalculateGoldReward(avgDefenderLevel, 1);
        int exp = CombatCalculator.CalculateExperienceReward(avgDefenderLevel, 1);

        foreach (Hero attacker in attackers)
        {
            attacker.GainExperience(exp);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResourceManager.AddGold(gold);
        }
    }

    public bool IsInCombat() => isInCombat;
}
