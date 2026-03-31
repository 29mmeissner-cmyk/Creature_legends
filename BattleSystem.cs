using UnityEngine;
using System.Collections.Generic;

public class BattleSystem : MonoBehaviour
{
    public Creature playerCreature;
    public Creature enemyCreature;
    public bool battleActive = false;
    private int turnCount = 0;

    public void StartBattle(Creature player, Creature enemy)
    {
        playerCreature = player;
        enemyCreature = enemy;
        battleActive = true;
        turnCount = 0;
        Debug.Log($"Battle started! {player.name} vs {enemy.name}");
    }

    public void PlayerAttack(Move move)
    {
        if (!battleActive || !move.HasPP()) return;

        turnCount++;
        move.UsePP();

        // Determine turn order based on speed
        bool playerFirst = playerCreature.speed >= enemyCreature.speed;

        if (playerFirst)
        {
            ExecuteAttack(playerCreature, enemyCreature, move);
            if (enemyCreature.IsAlive())
            {
                ExecuteRandomEnemyAttack();
            }
        }
        else
        {
            ExecuteRandomEnemyAttack();
            if (playerCreature.IsAlive())
            {
                ExecuteAttack(playerCreature, enemyCreature, move);
            }
        }

        CheckBattleEnd();
    }

    private void ExecuteAttack(Creature attacker, Creature defender, Move move)
    {
        // Check accuracy
        if (Random.Range(0, 100) > move.accuracy)
        {
            Debug.Log($"{attacker.name}'s {move.name} missed!");
            return;
        }

        int damage = CalculateDamage(attacker, defender, move);
        defender.TakeDamage(damage);

        Debug.Log($"{attacker.name} used {move.name}! Dealt {damage} damage.");
        Debug.Log($"{defender.name} has {defender.health}/{defender.maxHealth} HP");

        // Apply status effects
        ApplyMoveEffect(attacker, defender, move);
    }

    private void ExecuteRandomEnemyAttack()
    {
        if (enemyCreature.moves.Count > 0)
        {
            Move randomMove = enemyCreature.moves[Random.Range(0, enemyCreature.moves.Count)];
            if (randomMove.HasPP())
            {
                ExecuteAttack(enemyCreature, playerCreature, randomMove);
            }
        }
    }

    private int CalculateDamage(Creature attacker, Creature defender, Move move)
    {
        if (move.category == Move.MoveCategory.Status)
            return 0;

        float baseMultiplier = 1f;

        // Type effectiveness
        baseMultiplier *= GetTypeEffectiveness(move.type, defender.type);

        // Physical vs Special
        float attackStat = (move.category == Move.MoveCategory.Physical) ? attacker.attack : attacker.spAtk;
        float defenseStat = (move.category == Move.MoveCategory.Physical) ? defender.defense : defender.spDef;

        int damage = (int)((((2f * attacker.level / 5f + 2f) * move.power * (attackStat / defenseStat)) / 50f + 2f) * baseMultiplier);
        damage += Random.Range(-5, 5); // Variance

        return Mathf.Max(1, damage);
    }

    private float GetTypeEffectiveness(string attackType, string defendType)
    {
        // Type chart
        Dictionary<string, List<string>> superEffective = new Dictionary<string, List<string>>
        {
            { "Fire", new List<string> { "Nature", "Metal", "Ice" } },
            { "Water", new List<string> { "Fire", "Stone", "Ground" } },
            { "Electric", new List<string> { "Water", "Flying" } },
            { "Nature", new List<string> { "Water", "Ground", "Stone" } },
            { "Ice", new List<string> { "Flying", "Ground", "Nature" } },
            { "Fighting", new List<string> { "Stone", "Dark", "Metal" } },
            { "Flying", new List<string> { "Fighting", "Nature", "Bug" } },
            { "Stone", new List<string> { "Fire", "Flying", "Bug", "Ice" } },
            { "Ground", new List<string> { "Fire", "Electric", "Stone", "Metal" } },
            { "Metal", new List<string> { "Ice", "Stone", "Light" } },
            { "Light", new List<string> { "Dark" } },
            { "Dark", new List<string> { "Light", "Fighting" } }
        };

        if (superEffective.ContainsKey(attackType) && superEffective[attackType].Contains(defendType))
            return 1.6f;

        if (superEffective.ContainsKey(defendType) && superEffective[defendType].Contains(attackType))
            return 0.625f;

        return 1f;
    }

    private void ApplyMoveEffect(Creature attacker, Creature defender, Move move)
    {
        if (Random.Range(0f, 1f) > move.effectChance)
            return;

        switch (move.effect)
        {
            case Move.MoveEffect.Paralyze:
                Debug.Log($"{defender.name} was paralyzed!");
                break;
            case Move.MoveEffect.Burn:
                Debug.Log($"{defender.name} was burned!");
                break;
            case Move.MoveEffect.Poison:
                Debug.Log($"{defender.name} was poisoned!");
                break;
        }
    }

    private void CheckBattleEnd()
    {
        if (!playerCreature.IsAlive())
        {
            EndBattle(false);
        }
        else if (!enemyCreature.IsAlive())
        {
            EndBattle(true);
        }
    }

    private void EndBattle(bool playerWon)
    {
        battleActive = false;
        if (playerWon)
        {
            Debug.Log($"Victory! {enemyCreature.name} fainted!");
            playerCreature.GainExperience(150);
        }
        else
        {
            Debug.Log($"Defeat! {playerCreature.name} fainted!");
        }
    }
}