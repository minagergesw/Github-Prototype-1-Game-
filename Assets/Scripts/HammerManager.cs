using UnityEngine;

public class HammerManager : MonoBehaviour
{
  public static Transform HitPoint;

    [SerializeField] private Transform hitPoint;

    void Awake()
    {
        HitPoint = hitPoint;
    }
}
