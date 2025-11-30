using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelProgress : MonoBehaviour
{
    public static LevelProgress Instance { get; private set; }

    public Slider progressBar;
    public TextMeshProUGUI levelText;

    public int currentLevel = 1;
    private int totalObjectsPerLevel = 0;
    private int objectsDestroyed = 0;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // لو فيه نسخة تانية مسبقة
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // لو عايز يكمل بين المشاهد
    }

    void Start()
    {
        currentLevel = SaveManager.LoadLevel();


    }

    public void RegisterObject()
    {
        totalObjectsPerLevel++;
         UpdateUI();
    }
    public void ObjectDestroyed()
    {
        objectsDestroyed++;
        UpdateProgress();

        if (objectsDestroyed >= totalObjectsPerLevel)
        {
            NextLevel();
        }
    }

    void UpdateProgress()
    {
      if (progressBar != null)
    {
        progressBar.value = Mathf.Clamp01((float)objectsDestroyed / totalObjectsPerLevel);
        progressBar.onValueChanged.Invoke(progressBar.value); // يرسمه لو Unity مش حدث تلقائي
    }
        Debug.Log("" + progressBar.value);
         UpdateUI();
    }

    void NextLevel()
    {
        currentLevel++;
        objectsDestroyed = 0;
        totalObjectsPerLevel = 0;
 

        // لو عايز تغيّر عدد الأشياء لكل level:
        // totalObjectsPerLevel = ... 
    }
   public void retryLevelProgressBar()
    {
        objectsDestroyed = 0;
        
    }
   public void UpdateUI()
    {
        progressBar.value = (float)objectsDestroyed / totalObjectsPerLevel; 
        levelText.text = "Level " + currentLevel;
    }
}