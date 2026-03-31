using UnityEngine;

public class WildCatchingSystem : MonoBehaviour
{
    public float catchRate = 0.4f;
    public bool TryCatch(Creature wildCreature)
    {
        float chance = Random.value;
        return chance < catchRate;
    }
}