using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static void SaveProgress(int cash, int level, int damageLevel)
    {
        PlayerPrefs.SetInt("Cash", cash);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("DamageLevel", damageLevel);

        PlayerPrefs.Save(); // مهم جدًا
        Debug.Log("Progress Saved!");
    }
    public static void SaveCash(int cash)
    {
        PlayerPrefs.SetInt("Cash", cash);

        PlayerPrefs.Save(); // مهم جدًا
        Debug.Log("Cash Saved!");
    }
    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);

        PlayerPrefs.Save(); // مهم جدًا
        Debug.Log("Level Saved!");
    }

    public static int LoadCash()
    {
        return PlayerPrefs.GetInt("Cash", 0); // 0 قيمة افتراضية لو مفيش
    }

    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt("Level", 0);
    }

    public static int LoadDamageLevel()
    {
        return PlayerPrefs.GetInt("DamageLevel", 1); // يبدأ من 1
    }
}
