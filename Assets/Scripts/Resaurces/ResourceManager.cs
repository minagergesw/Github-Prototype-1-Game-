using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{


    public int baseMetalCapacity = 1000;
    public int baseEnergyCapacity = 10;
    public int baseNanoCapacity = 200;

    public int metalCapacityPerLevel = 500;
    public int energyCapacityPerLevel = 5;
    public int nanoCapacityPerLevel = 100;

    public int CurrentMetalCapacity =>
        baseMetalCapacity + GetGeneralUpgradeLevel("Metal Capacity") * metalCapacityPerLevel;

    public int CurrentEnergyCapacity =>
        baseEnergyCapacity + GetGeneralUpgradeLevel("Energy Capacity") * energyCapacityPerLevel;

    public int CurrentNanoCapacity =>
        baseNanoCapacity + GetGeneralUpgradeLevel("Nano Capacity") * nanoCapacityPerLevel;









    public static ResourceManager Instance;
    private Dictionary<ResourceData, int> resources = new Dictionary<ResourceData, int>();

    [System.Serializable]
    public class StartingResource
    {
        public ResourceData initialresource;
        public int amount;
    }

    public List<StartingResource> startingResources;



    void Awake()
    {
        Instance = this;

        foreach (var r in startingResources)
        {
            resources.Add(r.initialresource, r.amount);
        }
    }

    void Start()
    {
        foreach (var sr in startingResources)
        {
            resources[sr.initialresource] = sr.amount;
        }

        // Sync UI في أول ثانية
        foreach (var kvp in resources)
        {
            UIManager.Instance.UpdateResourceUI(kvp.Key, kvp.Value);
            Debug.Log("11111111");
        }
    }

    public int GetGeneralUpgradeLevel(string upgradeName)
    {
        foreach (var u in UpgradeManager.Instance.generalUpgrades.upgrades)
        {
            if (u.upgradeName == upgradeName)
                return u.level;
        }
        return 0;
    }

    public void AddResource(ResourceData data, int amount)
    {
        if (!resources.ContainsKey(data)) resources[data] = 0;
        int newValue = resources[data] + amount;

        int capacity = GetCapacityForResource(data);

        resources[data] = Mathf.Min(newValue, capacity);
        UIManager.Instance.UpdateResourceUI(data, resources[data]);
    }

    public int GetCapacityForResource(ResourceData data)
    {
        if (data.resourceName == "ScrapMetal")
            return CurrentMetalCapacity;

        if (data.resourceName == "EnergyCell")
            return CurrentEnergyCapacity;

        if (data.resourceName == "NanoParticle")
            return CurrentNanoCapacity;

        return 999999;
    }
    public void RecalculateCapacities()
{
    foreach (var data in resources.Keys)
    {
        int cap = GetCapacityForResource(data);
        if (resources[data] > cap)
            resources[data] = cap;

        UIManager.Instance.UpdateResourceUI(data, resources[data]);
    }
}
    public int GetAmount(ResourceData data) => resources.ContainsKey(data) ? resources[data] : 0;
    public int GetResource(ResourceData data)
    {
        if (resources.ContainsKey(data))
            return resources[data];
        return 0;
    }

    public bool SpendResource(ResourceData data, int amount)
    {
        if (resources.ContainsKey(data) && resources[data] >= amount)
        {
            resources[data] -= amount;
            UIManager.Instance.UpdateResourceUI(data, resources[data]);
            return true;
        }
        return false;
    }
}
