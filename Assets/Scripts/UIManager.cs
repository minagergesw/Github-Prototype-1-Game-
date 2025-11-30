using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [System.Serializable]
    public class ResourceUI
    {
        public ResourceData resourceData;
        public RectTransform root;   // ← UI container

        public Slider bar;
        public TMP_Text barText;
        public Image icon;
    }

    public List<ResourceUI> resourceUIList = new List<ResourceUI>();
    private Dictionary<ResourceData, ResourceUI> uiLookup = new Dictionary<ResourceData, ResourceUI>();
    void Awake()
    {
        Instance = this;

        // نحول اللستة إلى Dictionary
        foreach (var ui in resourceUIList)
        {
            if (ui.resourceData != null)
            {
                uiLookup[ui.resourceData] = ui;

            }

        }


    }


    public void UpdateResourceUI(ResourceData data, int amount)
    {
        if (!uiLookup.ContainsKey(data)) return;

        var ui = uiLookup[data];

        // احصل على الماكس من ResourceManager
        int max = ResourceManager.Instance.GetCapacityForResource(data);

        // ضبط البار
        ui.bar.maxValue = max;
        ui.bar.value = amount;

        // كتابة text جوه البار
        ui.barText.text = $"{amount} / {max}";
    }

    // public void UpdateUpgradeUI(UpgradeData upgrade)
    // {
    //     // مبدئيًا بس لوج للتجربة
    //     Debug.Log($"Upgraded {upgrade.upgradeName} to Level {upgrade.level}");

    //     // لاحقًا هنعمل ربط فعلي بعناصر UI (مثل نص المستوى أو الزر)
    // }



    public RectTransform GetResourceUI(ResourceData data)
    {
        if (uiLookup.ContainsKey(data))
            return uiLookup[data].root;

        return null;
    }






}
