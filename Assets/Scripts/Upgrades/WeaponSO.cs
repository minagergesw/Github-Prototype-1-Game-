using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
      [Header("Identity")]
    public string weaponID; // unique id - اكتب قيمة فريدة لكل WeaponSO
    public string weaponName;
    public Sprite icon;

    [Header("Prefab")]
    public GameObject prefab;

    [Header("Shop")]
    public int price = 100;
    public bool unlockByDefault = false; // لو عايز بعض الاسلحة مفتوحة من البداية

    [Header("Grip Options")]
    public bool useGripFromPrefab = true; 
    // لو false، يتم استخدام القيم الآتية:
    public Vector3 gripLocalPosition = Vector3.zero;
    public Vector3 gripLocalEuler = Vector3.zero;
    public Vector3 gripLocalScale = Vector3.one;

    [Header("Animation")]
    public RuntimeAnimatorController animatorOverride; // optional

    [Header("Stats (example)")]
    public int baseDamage = 10;
    public float fireRate = 1f;

    // الترقيات الخاصة بالسلاح فقط
    public UpgradeCategorySO upgradeCategory;
}
