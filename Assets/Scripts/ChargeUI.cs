using UnityEngine;
using UnityEngine.UI;

public class ChargeUI : MonoBehaviour
{
    public Image chargeImage;
    public float chargeSpeed = 1f;

    bool isCharging;

    void Update()
    {
        if (isCharging)
        {
            chargeImage.fillAmount += Time.deltaTime * chargeSpeed;
            chargeImage.fillAmount = Mathf.Clamp01(chargeImage.fillAmount);
        }
    }

    public void StartCharge()
    {
        isCharging = true;
        chargeImage.fillAmount = 0;
    }

    public void StopCharge()
    {
        isCharging = false;
    }
}
