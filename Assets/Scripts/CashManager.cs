using UnityEngine;
using TMPro;

public class CashManager : MonoBehaviour
{
    public static CashManager Instance;

    [Header("UI Reference")]
    public TextMeshProUGUI cashText;

    [Header("Starting Cash")]
    public int startingCash = 0;

    private int currentCash;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // تحميل الكاش من التخزين
        currentCash = PlayerPrefs.GetInt("Cash", startingCash);
    }

    private void Start()
    {
        UpdateCashUI();
    }

    public void AddCash(int amount)
    {
        currentCash += amount;
        SaveCash();
        UpdateCashUI();
    }

    public bool SpendCash(int amount)
    {
        if (currentCash < amount)
            return false;

        currentCash -= amount;
        SaveCash();
        UpdateCashUI();
        return true;
    }

    private void UpdateCashUI()
    {
        if (cashText != null)
            cashText.text = "$" + currentCash.ToString();
    }

    private void SaveCash()
    {
        SaveManager.SaveCash(currentCash);
    }

    public int GetCash()
    {
        return currentCash;
    }
}
