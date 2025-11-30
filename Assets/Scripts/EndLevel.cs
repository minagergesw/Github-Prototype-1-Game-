using UnityEngine;

public class EndLevel : MonoBehaviour
{


    public GameManager gameManager;
    private HealthSystem healthComponent;

    void Awake()
    {
        healthComponent = gameObject.GetComponent<HealthSystem>();
    }

    public void endLevel()
    {
        
            gameManager.EndLevel();
        
    }




}
