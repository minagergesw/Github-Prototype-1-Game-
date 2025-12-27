using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    private Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    public void EnableHitbox()
    {
        col.enabled = true;
    }

    public void DisableHitbox()
    {
        col.enabled = false;
    }
}
