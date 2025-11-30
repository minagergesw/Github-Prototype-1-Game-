using UnityEngine;

public class AdsManager : MonoBehaviour
{
 public static AdsManager Instance;

    void Awake()
    {
         if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // يخلي الأوبجكت يعيش بين المشاهد
    }
    else
    {
        Destroy(gameObject); // يمنع تكرار المانجر
    }
    }

    public void ShowInterstitial()
    {
        Debug.Log("Interstitial Ad Shown");
        // هنا تحط كود الـ SDK الخاص بالإعلانات
    }

    public void ShowRewarded()
    {
        Debug.Log("Rewarded Ad Shown");
        // هنا برضه تحط كود الـ SDK
    }
}
