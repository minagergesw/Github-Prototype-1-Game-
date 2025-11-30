using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    void Start()
    {
        LevelManager.Instance.RegisterObject();
        LevelProgress.Instance.RegisterObject();
    }

    public void Break()
    {
        // هنا تعمل التكسير أو الانفجار
        LevelManager.Instance.ObjectDestroyed();
        LevelProgress.Instance.ObjectDestroyed();

        Destroy(gameObject);
        OnDestroyed();
    }
        public List<ResourceData> possibleDrops;

    public void OnDestroyed() {
        foreach (var res in possibleDrops)
        {
            int amount = Random.Range(res.minDrop, res.maxDrop + 1);
            // ResourceManager.Instance.AddResource(res, amount);
            AutoCollectSpawner.Instance.SpawnCollect(res, amount, transform.position);
        }
    }
}
