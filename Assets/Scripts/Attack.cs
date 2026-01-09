using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Attack : MonoBehaviour
{
    public float comboResetTime = 1.0f; // الوقت المسموح بين الضربات
    private int comboStep = 0;          // رقم الضربة الحالية فالكومبو
    private float lastAttackTime = 0f;
    public InputActionAsset InputActions;
    private InputAction attackAction;
    private InputAction chargeAttackAction;
    private Animator animator;
    private WeaponStats weaponStats;

    [SerializeField] private GameObject weaponHolder;
    private BoxCollider weaponCollider;
    private ChargeAttack chargeAttack;

    public WeaponStats GetCurrentWeaponStats()
    {
        if (weaponHolder == null) return null;

        return weaponHolder.GetComponentInChildren<WeaponStats>();
    }

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        attackAction = InputActions.FindAction("Attack");
        chargeAttackAction = InputActions.FindAction("Charge Attack");

        chargeAttack = GetComponent<ChargeAttack>();

        Debug.Log(attackAction);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (attackAction.WasPressedThisFrame())
        {

            attack();

        }


        if (chargeAttackAction.WasPressedThisFrame())
        {
            Debug.Log("Begin ...");
            chargeAttack.StartCharge();
            chargeAttack.UpdateUI();
        }

        if (chargeAttackAction.IsPressed())
        {
            Debug.Log("Charging ...");

            chargeAttack.UpdateCharge();
            chargeAttack.UpdateUI();
        }
        if (chargeAttackAction.WasReleasedThisFrame())
        {
            Debug.Log("Finished ...");
            chargeAttack.ReleaseCharge();
            chargeAttack.ResetUI();

        }
    }


    public void attack()
    {

        float timeSinceLastAttack = Time.time - lastAttackTime;
        // comboResetTime = 1f / weaponStats.AttackSpeed;

        if (timeSinceLastAttack > comboResetTime)
        {
            comboStep = 0; // رجّع الكومبو للبداية
        }

        comboStep++;
        lastAttackTime = Time.time;
        Debug.Log("Compo : " + comboStep);

        // شغل أنيميشن حسب الخطوة الحالية
        animator.SetInteger("ComboStep", comboStep);
        animator.SetTrigger("Attack");

        // بعد آخر ضربة رجّع للخطوة الأولى تاني
        if (comboStep >= 3) // لو عندك 3 ضربات مثلا
        {
            comboStep = 0;
        }
        //  animator.SetTrigger("Attack");

    }
    public void EnableWeaponCollider()
    {
        weaponCollider = weaponHolder.GetComponentInChildren<BoxCollider>();
        weaponStats = weaponHolder.GetComponentInChildren<WeaponStats>();
        weaponCollider.size *= weaponStats.Range;
        weaponCollider.enabled = true;
    }
    public void DisableWeaponCollider()
    {
        weaponCollider = weaponHolder.GetComponentInChildren<BoxCollider>();
        weaponCollider.enabled = false;
        EndChargedAttack();


    }
    // public void NormalAttack()
    // {
    //     animator.SetTrigger("Attack");
    // }

    public void ChargedAttack()
    {
        animator.SetTrigger("ChargeAttack");
    }
    public void EndChargedAttack()
    {
        WeaponStats stats = GetCurrentWeaponStats();
        if (stats != null)
            stats.RestoreBaseStats();

        Debug.Log("Restored Damage ...");
    }
}
