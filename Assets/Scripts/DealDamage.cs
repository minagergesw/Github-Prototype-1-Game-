using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class DealDamage : MonoBehaviour
{

    public GameObject DamageVFX;

    [SerializeField] private float damage;


    private CinemachineImpulseSource hitShake;
    void Awake()
    {
        hitShake = GetComponent<CinemachineImpulseSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) { }
        HealthSystem enemy = other.GetComponent<HealthSystem>();
        enemy.TakeDamage(damage);
        hitShake.GenerateImpulse();
        if (DamageVFX != null)
        {
            GameObject DamVFX = Instantiate(DamageVFX) as GameObject;
            DamVFX.transform.position = GameObject.Find("HitPoint").transform.position;
            Destroy(DamVFX, 7);
        }
    }


}
