using UnityEngine;

[System.Serializable]
public class Ability
{
    public int id;
    public string name;
    public string description;
    public AbilityEffect effect;

    public enum AbilityEffect
    {
        Static, // 30% chance to paralyze on contact
        Overgrow, // Grass moves stronger when low HP
        Blaze, // Fire moves stronger when low HP
        Torrent, // Water moves stronger when low HP
        SwiftSwim, // Speed doubles in rain
        ChoiceScarf, // Speed boost
        FlashFire, // Fire type immunity, power boost
        ShieldDust, // Block secondary effects
        Intimidate, // Lower opponent attack on entry
        Regenerator // Heal 33% HP when switched out
    }

    public Ability(int abilityId, string abilityName, string abilityDesc, AbilityEffect abilityEffect)
    {
        id = abilityId;
        name = abilityName;
        description = abilityDesc;
        effect = abilityEffect;
    }
}
