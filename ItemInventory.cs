using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public List<string> items = new List<string>();
    public void AddItem(string item)
    {
        items.Add(item);
    }
    public bool RemoveItem(string item)
    {
        return items.Remove(item);
    }
}