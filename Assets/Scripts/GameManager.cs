using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int score = 0;
    public int cash = 0;

    public int bonus = 0;
    public float slowFactor = 0.2f;     // السرعة أثناء الـ slow motion
    public float slowDuration = 0.3f;   // مدة الـ slow motion (بالثواني)
    public float normalFactor = 1f;

    public TMP_Text scoreText;
    public TMP_Text endScoreText;
    public TMP_Text endCashText;

    public GameObject endScreen;



    public int extraScorePerHit = 0;
    public float cashBonusPercent = 0f;



    public static GameManager instance;
    public GameObject gameOverPanel; // اسحبها من الـ Inspector
    void Start()
    {
        if (GameMode.CurrentMode == GameMode.Mode.Timed)
        {
            //   StartTimer();
        }
        else
        {
            // Free Mode: لا تايمر
        }

        // تحميل البيانات
        cash = SaveManager.LoadCash();

        Debug.Log("Loaded Cash: " + cash);
    }



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // يفضل موجود
        }
        else
        {
            Destroy(gameObject); // لو فيه نسخة قديمة ما يعملش duplications
        }
    }

    // Call this when object breaks
    public void AddScore(int amount)
    {
        amount += extraScorePerHit; // يزود السكور لو فيه Upgrade

        score += amount;
        scoreText.text = "Score: " + score;
    }


    // Call this when level ends
    public void EndLevel()
    {
        int gainedCash = 0;
        if (LevelManager.Instance.retryLevelbool)
        {
          
               
             //////////////////////////////////////
                
                bonus = Mathf.Clamp(30, 0, 35); // مثال
                cash += bonus;
                endCashText.text = "Bonus Earned: " + bonus + "\nTotal Cash: " + cash;
          
        }
        else
        {
            gainedCash = score / 10; // تحويل score لكاش

            // إضافة البونص من الترقية
            gainedCash += Mathf.RoundToInt(gainedCash * cashBonusPercent);

            cash += gainedCash;
            endCashText.text = "Cash Earned: " + gainedCash + "\nTotal Cash: " + cash;
        }


        SaveManager.SaveCash(cash);
        SaveManager.SaveLevel(LevelManager.Instance.currentLevelIndex);
        // Update End Screen
        endScoreText.text = "Score: " + score;
       


        endScreen.SetActive(true);
    }

    public void GameOver()
    {
        // وقف الوقت في اللعبة
        Time.timeScale = 0f;

        // أظهر شاشة الـ Game Over
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log("Game Over!");
    }
    // Call this when level ends
    public void HideEndScrean()
    {

        score = 0;
        endScreen.SetActive(false);
        scoreText.text = "Score: " + score;

    }
    public void RetryLevelResetValues()
    {
        // reset score & reload scene
        score = 0;
        scoreText.text = "Score: " + score;
        LevelProgress.Instance.retryLevelProgressBar();


    }
    public void HideGameOver()
    {

        score = 0;

        gameOverPanel.SetActive(false);
        scoreText.text = "Score: " + score;

    }
    public void SlowMotion()
    {
        StartCoroutine(SlowMotionRoutine());
    }
    private IEnumerator SlowMotionRoutine()
    {
        normalFactor = Time.fixedDeltaTime;
        // نزّل السرعة
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // علشان الفيزيكس يشتغل مظبوط
        Debug.Log("Slow motion start");
        Debug.Log("TimeScale Now: " + Time.timeScale);
        yield return new WaitForSeconds(slowDuration); // Realtime عشان ما يتأثرش بـ timeScale
        Debug.Log("Slow motion end");
        Debug.Log("TimeScale Now: " + Time.timeScale);
        // رجّع الوضع الطبيعي
        Time.timeScale = 1f;
        Time.fixedDeltaTime = normalFactor;

    }

}
