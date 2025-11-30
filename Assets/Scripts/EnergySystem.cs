using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float energyPerHit = 5f;

    void Start() => currentEnergy = maxEnergy;

    public bool CanPerformAction(float cost) => currentEnergy >= cost;

    public void UseEnergy(float cost) {
        if (CanPerformAction(cost))
        {
        
        currentEnergy -= cost;
      }
        if (currentEnergy <= 0)
        {
          ///  RobotManager.Instance.ReturnToBaseForRecharge();
        }
    }

    public void Recharge() {
        currentEnergy = maxEnergy;
    }
}
