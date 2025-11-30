using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelector : MonoBehaviour
{
  public void SelectFreeMode()
    {
        GameMode.CurrentMode = GameMode.Mode.Free;
        SceneManager.LoadScene("Playground");
    }

    public void SelectTimedMode()
    {
        GameMode.CurrentMode = GameMode.Mode.Timed;
        SceneManager.LoadScene("Playground");
    }
}
