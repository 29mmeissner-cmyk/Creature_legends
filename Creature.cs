using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Creature
{
    public int id;
    public string name;
    public string type; // e.g., "Fire", "Water", "Electric", "Nature", "Stone", "Light", "Dark", "Metal"
    public int level;
    public int experience;
    public int health;
    public int maxHealth;
    public int attack;
    public int defense;
    public int spAtk;
    public int spDef;
    public int speed;
    public List<Move> moves = new List<Move>();
    public Ability ability;
    public string nature; // Affects stat growth
    public List<int> evolutionLine; // IDs of evolved forms
    public Sprite artwork;
    public string description;

    public Creature(int creatureId, string creatureName, string creatureType)
    {
        id = creatureId;
        name = creatureName;
        type = creatureType;
        level = 1;
        experience = 0;
        maxHealth = 45;
        health = maxHealth;
        attack = 49;
        defense = 49;
        spAtk = 65;
        spDef = 65;
        speed = 45;
    }

    public void GainExperience(int amount)
    {
        experience += amount;
        
        // Level up every 1000 exp
        int levelsGained = experience / 1000;
        if (levelsGained > 0)
        {
            level += levelsGained;
            experience %= 1000;
            UpdateStats();
        }
    }

    public void UpdateStats()
    {
        float levelMultiplier = 1f + (level * 0.1f);
        maxHealth = (int)(45 * levelMultiplier);
        health = maxHealth;
        attack = (int)(attack * levelMultiplier);
        defense = (int)(defense * levelMultiplier);
        spAtk = (int)(spAtk * levelMultiplier);
        spDef = (int)(spDef * levelMultiplier);
        speed = (int)(speed * levelMultiplier);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}