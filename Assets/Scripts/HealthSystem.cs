using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public GameObject fructuredObject;
    public GameObject explosionVFX;
    public float explosionMinForce = 5;
    public float explosionMaxForce = 100;
    public float explosionForceRadius = 10;
    public float fragScaleFactor = 1;

    private GameObject fractObj;

    private AudioSource audioSource;
    public AudioClip damageClip;
    private AudioClip explodeClip;
    private CinemachineImpulseSource explosionshake;

    private float crackValue = 0f;

    public float slowFactor = 0.2f;     // السرعة أثناء الـ slow motion
    public float slowDuration = 0.3f;   // مدة الـ slow motion (بالثواني)
    public float normalFactor = 1f;
    private int objectScore=0;

    private GameManager gameManager;



    void Awake()
    {
        explosionshake = GameManager.instance.GetComponent<CinemachineImpulseSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        gameManager.AddScore(Random.Range(10, 100));
        AddCrack((damage / health) * 100);
        Debug.Log(health);
        audioSource = this.gameObject.GetComponent<AudioSource>();

        audioSource.PlayOneShot(damageClip);
        if (health <= 0)
        {
            Explode();
        }

        Handheld.Vibrate(); //vibrate for mobile
    }

    public void Explode()
    {
        GameManager.instance.SlowMotion();
        Handheld.Vibrate();//vibrate for mobile
        objectScore = Random.Range(100, 150);
         gameManager.AddScore(objectScore);




    


        audioSource = GameObject.Find("PlayerArmature").GetComponent<AudioSource>();
        audioSource.Play();
        explosionshake.GenerateImpulse();
        gameObject.GetComponent<BreakableObject>().Break();
        if (explosionVFX != null)
        {
            GameObject exploVFX = Instantiate(explosionVFX) as GameObject;
            exploVFX.transform.position = HammerManager.HitPoint.position;
            Destroy(exploVFX, 7);
        }
        if (fructuredObject != null)
        {
            fractObj = Instantiate(fructuredObject, transform.position, transform.rotation) as GameObject;

            foreach (Transform t in fractObj.transform)
            {

                var rb = t.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(Random.Range(explosionMinForce, explosionMaxForce), transform.position, explosionForceRadius);

                StartCoroutine(Shrink(t, 2));

            }
            Destroy(fractObj, 5);




        }



        this.gameObject.SetActive(false);
    }


    public void AddCrack(float amount)
    {
        Renderer rend = GetComponent<Renderer>();
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        rend.GetPropertyBlock(block);

        crackValue += amount;
        crackValue = Mathf.Clamp01(crackValue);
        block.SetFloat("_CracksAmount", crackValue);   //اسم البراميتر في الكود لازم يطابق الـ Reference Name اللي في Shader Graph (مش الاسم العادي).تلاقيه في الـ Blackboard → CrackAmount → تحتها مكتوب Reference (مثلاً: _CrackAmount).

        rend.SetPropertyBlock(block);
    }
    IEnumerator Shrink(Transform t, int index)
    {
        yield return new WaitForSeconds(index);
        Vector3 newScale = t.localScale;
        while (newScale.x >= 0)
        {
            newScale -= new Vector3(fragScaleFactor, fragScaleFactor, fragScaleFactor);
            t.localScale = newScale;
            yield return new WaitForSeconds(0.05f);
        }


    }

}
