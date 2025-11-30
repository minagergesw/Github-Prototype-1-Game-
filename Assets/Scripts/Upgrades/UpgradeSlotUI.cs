using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class UpgradeSlotUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text upgradeNameText;
    public TMP_Text levelText;
    public TMP_Text costText;
    public TMP_Text descriptionText;
    public Button upgradeButton; // نسيبها Button عادي لأن TMP Button بيورث منه

    [HideInInspector] public UpgradeData upgradeData;

    private void Start()
    {
        if (upgradeButton != null)
            upgradeButton.onClick.AddListener(OnUpgradeClicked);
    }

    public void Setup(UpgradeData data)
    {
        upgradeData = data;
        Refresh();
    }

    public void Refresh()
    {
        if (upgradeData == null) return;

        upgradeNameText.text = upgradeData.upgradeName;
        levelText.text = $"Lv. {upgradeData.level+1}";

        // بناء نص التكلفة بعدد موارد متعدد
        StringBuilder sb = new StringBuilder("<b>Cost:</b> ");
        foreach (var cost in upgradeData.costList)
        {
            int required = upgradeData.GetCostForResource(cost.resource);
            int available = ResourceManager.Instance.GetResource(cost.resource);

            // لو مش كفاية → باللون الأحمر
            string color = available >= required ? "green" : "red";
            sb.Append($"<color={color}>{required} {cost.resource.resourceName}</color>  ");
        }

        costText.text = sb.ToString();
    }

    void OnUpgradeClicked()
    {
        if (UpgradeManager.Instance.TryUpgrade(upgradeData))
            Refresh();
        else
            Refresh(); // عشان تحدث اللون لو الموارد اتخصمت جزئياً
    }
}
