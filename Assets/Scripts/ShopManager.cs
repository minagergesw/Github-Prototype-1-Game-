using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameManager gm;

    public GameObject shopPanel;
    public TMP_Text scorePowerText;
    public TMP_Text cashBonusText;

    private int scorePowerLevel = 0;
    private int cashBonusLevel = 0;

    private int scorePowerCost = 100;
    private int cashBonusCost = 200;
    public static ShopManager instance;
public WeaponSO weaponSO;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // يفضل موجود
        }
        else
        {
            Destroy(gameObject); // لو فيه نسخة قديمة ما يعملش duplications
        }
    }
    public void OpenShop()
    {
        shopPanel.SetActive(true);
        UpdateUI();
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }
    public void BuyScorePower()
    {
        // if (gm.cash >= scorePowerCost)
        // {
        //     gm.cash -= scorePowerCost;
        //     scorePowerLevel++;
        //     scorePowerCost += 100; // السعر يزيد تدريجياً
        //     gm.extraScorePerHit += 5; // تأثير الترقية
        //     UpdateUI();
        // }
        PlayerWeaponController.Instance.EquipWeapon(weaponSO);

        
    }

    public void BuyCashBonus()
    {
        if (gm.cash >= cashBonusCost)
        {
            gm.cash -= cashBonusCost;
            cashBonusLevel++;
            cashBonusCost += 100;
            gm.cashBonusPercent += 0.1f; // تأثير الترقية
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        scorePowerText.text = $"Score Power (Lvl {scorePowerLevel}) - Cost: {scorePowerCost}";
        cashBonusText.text = $"Cash Bonus (Lvl {cashBonusLevel}) - Cost: {cashBonusCost}";
    }
}
