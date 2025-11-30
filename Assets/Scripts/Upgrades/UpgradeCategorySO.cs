using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeCategorySO", menuName = "Scriptable Objects/UpgradeCategorySO")]
public class UpgradeCategorySO : ScriptableObject
{
        public string categoryName;
    public Sprite categoryIcon;

    public UpgradeData[] upgrades;
}
