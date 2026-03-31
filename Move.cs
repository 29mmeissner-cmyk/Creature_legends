using UnityEngine;

[System.Serializable]
public class Move
{
    public int id;
    public string name;
    public string type;
    public int power; // Base damage
    public int accuracy; // 0-100
    public int pp; // Power Points (uses per battle)
    public int maxPp;
    public string description;
    public MoveCategory category; // Physical, Special, Status
    public MoveEffect effect; // What the move does
    public float effectChance; // Chance to apply effect (0-1)

    public enum MoveCategory
    {
        Physical,
        Special,
        Status
    }

    public enum MoveEffect
    {
        Damage,
        Heal,
        RaiseAttack,
        LowerAttack,
        RaiseDefense,
        LowerDefense,
        RaiseSpeed,
        LowerSpeed,
        Paralyze,
        Burn,
        Poison,
        Sleep,
        Confuse,
        MultiHit
    }

    public Move(int moveId, string moveName, string moveType, int movePower, int moveAccuracy, int movePp, MoveCategory cat, MoveEffect eff)
    {
        id = moveId;
        name = moveName;
        type = moveType;
        power = movePower;
        accuracy = moveAccuracy;
        pp = movePp;
        maxPp = movePp;
        category = cat;
        effect = eff;
        effectChance = 0.3f;
    }

    public void UsePP()
    {
        pp--;
        if (pp < 0) pp = 0;
    }

    public bool HasPP()
    {
        return pp > 0;
    }

    public void RestorePP()
    {
        pp = maxPp;
    }
}
