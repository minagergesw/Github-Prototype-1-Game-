using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static int currentLevel = 1;

    void Start()
    {
        currentLevel = SaveManager.LoadLevel();
        Debug.Log("Loaded Level: " + currentLevel);
    }
    public static void ResetProgress()
    {
        currentLevel = 1;
    }

    public static void NextLevel()
    {
        currentLevel++;
    }
}
