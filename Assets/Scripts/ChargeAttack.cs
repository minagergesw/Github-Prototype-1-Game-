using UnityEngine;
using UnityEngine.InputSystem;

public class ChargeAttack : MonoBehaviour
{
    [Header("Charge Settings")]
    public float maxChargeTime = 2f;
    public float minChargeToActivate = 0.5f;

    private float chargeTimer;
    private bool isCharging;

    private WeaponStats weaponStats;
    private Attack attack;

    void Awake()
    {
        weaponStats = GetComponentInChildren<WeaponStats>();
        attack = GetComponent<Attack>();
    }

    public void StartCharge()
    {
        isCharging = true;
        chargeTimer = 0f;
    }

    public void UpdateCharge()
    {
        if (!isCharging) return;

        chargeTimer += Time.deltaTime;
        chargeTimer = Mathf.Clamp(chargeTimer, 0, maxChargeTime);

        // هنا هنربط الـ Visual بعد شوية
    }

    public void ReleaseCharge()
    {
        isCharging = false;

        float chargePercent = chargeTimer / maxChargeTime;

        if (chargeTimer >= minChargeToActivate)
        {
            PerformChargedAttack(chargePercent);
        }
        else
        {
            attack.NormalAttack();
        }
    }

    void PerformChargedAttack(float chargePercent)
    {
        Debug.Log("Charged Attack: " + chargePercent);

        // مثال:
        weaponStats.ApplyChargeMultiplier(chargePercent);
        attack.ChargedAttack();
    }
}
