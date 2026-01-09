using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChargeAttack : MonoBehaviour
{
    [Header("Charge Settings")]
    public float maxChargeTime = 2f;
    public float minChargeToActivate = 0.5f;

    private float chargeTimer;
    private bool isCharging;

    private Attack attack;
    [Header("UI")]
    public Image chargeFillImage;
    public float chargeSpeed = 1f;



    void Awake()
    {
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
        Debug.Log("Current Time : " + chargeTimer);
        chargeTimer = Mathf.Clamp(chargeTimer, 0, maxChargeTime);
        Debug.Log("Current Time : " + chargeTimer);

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
            attack.attack();
        }

    }

    void PerformChargedAttack(float chargePercent)
    {
        WeaponStats stats = attack.GetCurrentWeaponStats();
        Debug.Log("Charged Attack: " + chargePercent);

        // مثال:
        stats.ApplyChargeMultiplier(chargePercent);
        attack.ChargedAttack();
    }
    void Update()
    {
        if (isCharging)
        {
            chargeFillImage.fillAmount += Time.deltaTime * chargeSpeed;
            chargeFillImage.fillAmount = Mathf.Clamp01(chargeFillImage.fillAmount);
        }
    }
    public void UpdateUI()
    {
        if (chargeFillImage != null)

        {
            chargeFillImage.fillAmount = chargeTimer / maxChargeTime;
            chargeFillImage.color = Color.Lerp(Color.yellow, Color.red, chargeTimer / maxChargeTime);
        }
    }

    public void ResetUI()
    {
        if (chargeFillImage != null)
            chargeFillImage.fillAmount = 0f;
    }
    
}
