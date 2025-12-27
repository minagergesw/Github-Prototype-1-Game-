using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class DealDamage : MonoBehaviour
{

    public GameObject DamageVFX;
    private WeaponStats weaponStats;
    // [SerializeField] private float damage;


    private CinemachineImpulseSource hitShake;
    void Awake()
    {
        hitShake = GetComponent<CinemachineImpulseSource>();
        weaponStats = GetComponentInParent<WeaponStats>();


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) { }
        HealthSystem enemy = other.GetComponent<HealthSystem>();
        float finalDamage = weaponStats.Damage;
Debug.Log("Damage : " + finalDamage);
        if (Random.value < weaponStats.CritChance)
        {
            finalDamage *= 2f;
        }
Debug.Log("Damage : " + finalDamage);

        enemy.TakeDamage(finalDamage);
        hitShake.GenerateImpulse();
        if (DamageVFX != null)
        {
            GameObject DamVFX = Instantiate(DamageVFX) as GameObject;
            DamVFX.transform.position = GameObject.Find("HitPoint").transform.position;
            Destroy(DamVFX, 7);
        }
    }


}
