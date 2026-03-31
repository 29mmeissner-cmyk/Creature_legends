using System.Collections.Generic;
using UnityEngine;

public class MoveDatabase : MonoBehaviour
{
    public List<Move> moves;
    public Move GetMove(string name)
    {
        return moves.Find(m => m.name == name);
    }
}