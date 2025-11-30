using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public float comboResetTime = 1.0f; // الوقت المسموح بين الضربات
    private int comboStep = 0;          // رقم الضربة الحالية فالكومبو
    private float lastAttackTime = 0f;
    public InputActionAsset InputActions;
    private InputAction attackAction;
    private Animator animator;

    [SerializeField] private Collider weaponCollider;


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
    }


    void attack()
    {
        
        float timeSinceLastAttack = Time.time - lastAttackTime;

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
        weaponCollider.enabled = true;
    }
    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
        

    }
}
