using UnityEngine;

public class AutoCollectSpawner : MonoBehaviour
{
    public static AutoCollectSpawner Instance;
    public GameObject collectPrefab;
    public Canvas mainCanvas; // اسحب له الـ Canvas بتاع UI
    void Awake() => Instance = this;

    public void SpawnCollect(ResourceData data, int amount, Vector3 worldPos)
    {
        RectTransform target = UIManager.Instance.GetResourceUI(data);
               // تحويل لمكان الشاشة
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        if (target == null)
        {
            Debug.LogWarning("Missing UI target for resource");
            return;
        }
        // إنستا من prefab جوه الكانفاس
        GameObject fx = Instantiate(collectPrefab, mainCanvas.transform);
        fx.transform.position = screenPos;

        fx.GetComponent<AutoCollectFX>().StartFlying(data, amount, worldPos, target);
    }
}
