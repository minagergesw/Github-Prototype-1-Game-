using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;


    [Header("Levels Setup")]
    public int currentLevelIndex = 0;       // هنبدأ من أول ليفل
    private GameObject currentLevelInstance; // علشان نمسك النسخة اللي شغالة
    public GameObject[] levelPrefabs; // كل التشكيلات الجاهزة
    private GameObject currentLevelObj;
    private int totalObjects = 0;
    private int destroyedObjects = 0;
    public bool retryLevelbool = false;


    void Start()
    {
       //  PlayerPrefs.GetInt("Level", 0);
        if (GameMode.CurrentMode == GameMode.Mode.Timed)
        {
            //    StartTimer();
        }
        else
        {
            // Free Mode: لا تايمر
        }
        currentLevelIndex = SaveManager.LoadLevel();
        LoadLevel(currentLevelIndex); // أول ما اللعبة تبدأ يفتح أول ليفل


    }

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
    // يستدعى لما جسم يتولد 
    public void RegisterObject()
    {
        totalObjects++;
    }

    // يستدعى لما جسم يتكسر
    public void ObjectDestroyed()
    {
        destroyedObjects++;

        if (destroyedObjects >= totalObjects)
        {
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        Debug.Log("Level Completed!");
        // هنا تقدر تعرض بانل النهاية
        GameManager.instance.EndLevel();

    }
    public void LoadNextLevel()
    {
        // Reset counter
        totalObjects = 0;
        destroyedObjects = 0;

        GameProgress.NextLevel();
        int currentLevel = GameProgress.currentLevel;

        // إعلان كل 2 مستوى
        if (currentLevel % 2 == 0)
        {
            ///////            AdsManager.Instance.ShowInterstitial();
        }

        // // تغيير العالم كل 10 مستويات
        // if (currentLevel % 10 == 0)
        // {
        //     string worldName = "World_" + (currentLevel / 10);
        //     SceneManager.LoadScene(worldName);
        //     Debug.Log("Level loaded: " + levelPrefabs[currentLevel].ToString());


        //     return;
        // }
        // SceneManager.sceneLoaded += OnSceneLoaded;

        // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        // {
        //     Debug.Log("Scene Loaded: " + scene.name);
        //     currentLevelIndex++;               // نزود رقم الليفل
        //     LoadLevel(currentLevelIndex);      // نجيب الليفل اللي بعده
        //     SceneManager.sceneLoaded -= OnSceneLoaded; // عشان ميتكررش
        // }
        if (currentLevelInstance != null)
        {
            Destroy(currentLevelInstance); // نمسح الليفل الحالي
            Debug.Log("Level Destroyed: " + levelPrefabs[currentLevel].ToString());

        }

        currentLevelIndex++;               // نزود رقم الليفل
        //int index = (currentLevel - 1) % levelPrefabs.Length;
        LoadLevel(currentLevelIndex);      // نجيب الليفل اللي بعده
        SaveManager.SaveLevel(LevelManager.Instance.currentLevelIndex);
        retryLevelbool = false;
        GameManager.instance.HideEndScrean();
    }
    void LoadLevel(int index)
    {
        if (index < levelPrefabs.Length)
        {
            currentLevelInstance = Instantiate(levelPrefabs[index], Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.Log("🎉 خلصت كل الليفلات!");
            // هنا ممكن تفتح شاشة "Game Completed"
        }
    }
    public void RetryLevel()
    {
        Destroy(currentLevelInstance);
        LoadLevel(currentLevelIndex);
        GameManager.instance.RetryLevelResetValues();
        GameManager.instance.HideEndScrean();
        retryLevelbool =true;
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Destroy(currentLevelInstance);
        LoadLevel(currentLevelIndex);
        GameManager.instance.RetryLevelResetValues();
        GameManager.instance.HideGameOver();
        
    }
  
}
