using System.Collections.Generic;
using UnityEngine;

public class ExpandedCreatureDex : MonoBehaviour
{
    public List<Creature> allCreatures;
    public Creature SearchCreature(string name)
    {
        return allCreatures.Find(c => c.name == name);
    }
}