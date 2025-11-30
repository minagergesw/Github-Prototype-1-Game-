
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoCollectFX : MonoBehaviour
{
    public RectTransform targetUI;     // لحد فين يطير (مكان الـ Resource UI)
    public float speed = 8f;
    public int amount;
    public ResourceData resource;
    public Image iconImage;
    private RectTransform rect;
    public TMP_Text amountText;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void StartFlying(ResourceData res, int amt, Vector3 worldPos, RectTransform target)
    {
        resource = res;
        amount = amt;
        targetUI = target;
        amountText.text = "+" + amount;
        iconImage.sprite = resource.icon;  // ← هنا الصورة تتغير حسب المورد
        // نحول مكان التكسير لمكان UI
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        rect.position = screenPos;
    }

    void Update()
    {
        if (targetUI == null) return;

        rect.position = Vector3.Lerp(rect.position, targetUI.position, Time.deltaTime * speed);

        float dist = Vector3.Distance(rect.position, targetUI.position);
        if (dist < 40f)
        {
            ResourceManager.Instance.AddResource(resource, amount);
            Destroy(gameObject);
        }
    }
}

