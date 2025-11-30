using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public Sprite icon;
    public int level = 0;
    public int maxLevel = 5;
public enum UpgradeType
{
    Global,         // ترقية عامة
    WeaponSpecific  // ترقية خاصة بسلاح
}

public UpgradeType upgradeType = UpgradeType.Global;

    [System.Serializable]
    public class UpgradeCost
    {
        public ResourceData resource;
        public int baseCost;
        public int startLevel = 0; // المستوى اللي يبدأ منه الحساب

    }



    [Header("Cost Settings")]
    public List<UpgradeCost> costList = new List<UpgradeCost>();
    public float costMultiplier = 1.5f;

    [Header("Upgrade Effect")]
    public float baseValue = 1f;
    public float valuePerLevel = 0.5f;

    public int GetCostForResource(ResourceData resource)
    {
        foreach (var cost in costList)
    {
        if (cost.resource == resource)
        {
            if (level < cost.startLevel)
                return 0; // ما يستخدمش المورد قبل المستوى المطلوب

            return Mathf.RoundToInt(cost.baseCost * Mathf.Pow(costMultiplier, level - cost.startLevel));
        }
    }
        return 0;
    }

    public float GetCurrentValue()
    {
        return baseValue + level * valuePerLevel;
    }
}
