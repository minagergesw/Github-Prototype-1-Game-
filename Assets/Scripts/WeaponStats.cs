using UnityEngine;
using UnityEngine.UIElements;

public class WeaponStats : MonoBehaviour
{

    private BoxCollider weaponCollider;
    private Vector3 baseColliderSize;
    private float baseRadius;

    [Header("Base Stats")]
    public float baseDamage = 10;
    public float baseAttackSpeed = 1f;
    public float baseRange = 1f;
    public float baseCritChance = 0.05f;

    [Header("Upgrade Scaling")]
    public float damagePerLevel = 2f;
    public float attackSpeedPerLevel = 0.1f;
    public float rangePerLevel = 0.1f;
    public float critPerLevel = 0.02f;

    public float Damage { get; private set; }
    public float AttackSpeed { get; private set; }
    public float Range { get; private set; }
    public float CritChance { get; private set; }
    private float originalDamage;
    private float originalRange;
    void Start()
    {
        RecalculateStats();
    }

    public void RecalculateStats()
    {
        int dmgLv = UpgradeManager.Instance.GetUpgradeLevel("Damage Upgrade");
        int rngLv = UpgradeManager.Instance.GetUpgradeLevel("Attack Radius");
        int crtLv = UpgradeManager.Instance.GetUpgradeLevel("Critical Hit");

        Damage = baseDamage + dmgLv * damagePerLevel;
        Range = baseRange + rngLv * rangePerLevel;
        CritChance = baseCritChance + crtLv * critPerLevel;
    }

    [Header("Charge Attack")]
    public float chargeDamageMultiplier = 2f;
    public float chargeRangeMultiplier = 1.5f;


  public void SaveBaseStats()
    {
        originalDamage = Damage;
        originalRange = Range;
    }

    public void RestoreBaseStats()
    {
        Damage = originalDamage;
        Range = originalRange;
    }
    public void ApplyChargeMultiplier(float percent)
    {

                SaveBaseStats();

        Damage *= Mathf.Lerp(1f, chargeDamageMultiplier, percent);
        Range *= Mathf.Lerp(1f, chargeRangeMultiplier, percent);
        
        Debug.Log("Released Charging");

    }

}
