using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;
    [Header("Upgrades UI")]
    public Transform upgradesParent;
    public GameObject upgradeSlotPrefab;
    public UpgradeCategorySO generalUpgrades;
    public WeaponSO currentWeapon;
    private List<UpgradeSlotUI> upgradeSlots = new List<UpgradeSlotUI>();
    public Button generalTabButton;
    public Button weaponsTabButton;
    public GameObject Panel;

    void Start()
    {
        generalTabButton.onClick.AddListener(() => ShowCategory(generalUpgrades));
        weaponsTabButton.onClick.AddListener(() =>
        {
            if (currentWeapon != null && currentWeapon.upgradeCategory != null)
                ShowCategory(currentWeapon.upgradeCategory);
        });
    }

    void Awake()
    {
        Instance = this;
    }

    public bool TryUpgrade(UpgradeData upgrade)
    {
        // 1. التحقق أن المستوى أقل من الحد الأقصى
        if (upgrade.level >= upgrade.maxLevel)
            return false;

        // 2. التأكد من توفر كل الموارد المطلوبة
        foreach (var cost in upgrade.costList)
        {
            int required = upgrade.GetCostForResource(cost.resource);
            int available = ResourceManager.Instance.GetResource(cost.resource);
            if (available < required)
            {
                Debug.Log($"Not enough {cost.resource.resourceName}");
                return false;
            }
        }

        // 3. خصم الموارد كلها
        foreach (var cost in upgrade.costList)
        {
            int required = upgrade.GetCostForResource(cost.resource);
            ResourceManager.Instance.SpendResource(cost.resource, required);
        }

        // 4. تنفيذ الترقية
        upgrade.level++;
        UpdateUpgradeUI(upgrade);
        ResourceManager.Instance.RecalculateCapacities();
        Debug.Log($"Upgraded {upgrade.upgradeName} to level {upgrade.level}");
        return true;
    }

    public void OpenPanel()
    {
        Panel.SetActive(true);


        ShowCategory(generalUpgrades); // أول فئة = General

        // List<UpgradeData> allUpgrades = new List<UpgradeData>();

        // // 1. الترقيات العامة
        // allUpgrades.AddRange(generalUpgrades.upgrades);

        // // 2. الترقيات الخاصة بالسلاح اللي اللاعب ماسكه
        // if (currentWeapon != null)
        //     allUpgrades.AddRange(currentWeapon.weaponUpgrades);

        // SetupUpgradesUI(allUpgrades);
    }

    public void ClosePanel()
    {
        Panel.SetActive(false);
        Debug.Log("Opened");
    }

    public void SetupUpgradesUI(List<UpgradeData> allUpgrades)
    {
        foreach (Transform child in upgradesParent)
            Destroy(child.gameObject);

        foreach (var upgrade in allUpgrades)
        {
            GameObject slot = Instantiate(upgradeSlotPrefab, upgradesParent);
            UpgradeSlotUI ui = slot.GetComponent<UpgradeSlotUI>();
            ui.Setup(upgrade);
            upgradeSlots.Add(ui);
        }
    }


    public void UpdateUpgradeUI(UpgradeData upgrade)
    {
        foreach (var slot in upgradeSlots)
        {
            if (slot.upgradeData == upgrade)
            {
                slot.Refresh();
                break;
            }
        }
    }
    public void ShowCategory(UpgradeCategorySO category)
    {
        // امسح القديم
        foreach (Transform child in upgradesParent)
            Destroy(child.gameObject);

        upgradeSlots.Clear();

        // اعرض الترقيات
        foreach (var upgrade in category.upgrades)
        {
            GameObject slot = Instantiate(upgradeSlotPrefab, upgradesParent);
            UpgradeSlotUI ui = slot.GetComponent<UpgradeSlotUI>();
            ui.Setup(upgrade);
            upgradeSlots.Add(ui);
        }
    }
}
