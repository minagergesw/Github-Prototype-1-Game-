using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public static PlayerWeaponController Instance;

    public Transform weaponHolder; // Assign WeaponHolder here in Inspector
    private GameObject currentWeaponInstance;
    private WeaponSO currentWeaponData;

    void Awake()
    {
        Instance = this;
    }

    public void EquipWeapon(WeaponSO weapon)
    {
        // Clear old weapon
        if (currentWeaponInstance != null)
            Destroy(currentWeaponInstance);

        currentWeaponData = weapon;

        // Instantiate weapon prefab
        currentWeaponInstance = Instantiate(weapon.prefab, weaponHolder);

        // Snap to grip
        Transform grip = currentWeaponInstance.transform.Find("Grip");

        if (grip != null)
        {
            currentWeaponInstance.transform.localPosition = Vector3.zero;
            currentWeaponInstance.transform.localRotation = Quaternion.Inverse(grip.localRotation);
        }
        else
        {
            Debug.LogWarning("No Grip found in weapon prefab!");
        }
    }
}
