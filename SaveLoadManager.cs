using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public void SaveGame()
    {
        PlayerPrefs.SetInt("HasSavedGame", 1);
        Debug.Log("Game saved!");
    }
    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("HasSavedGame", 0) == 1)
        {
            Debug.Log("Game loaded!");
        }
        else
        {
            Debug.Log("No game to load.");
        }
    }
}