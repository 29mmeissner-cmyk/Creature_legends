using System.Collections.Generic;
using UnityEngine;

public class PCStorage : MonoBehaviour
{
    public List<Creature> storedCreatures = new List<Creature>();
    public void StoreCreature(Creature creature)
    {
        storedCreatures.Add(creature);
    }
    public Creature RetrieveCreature(int index)
    {
        if (index >= 0 && index < storedCreatures.Count)
        {
            Creature c = storedCreatures[index];
            storedCreatures.RemoveAt(index);
            return c;
        }
        return null;
    }
}