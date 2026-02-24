using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    private void Start()
    {
       Time.timeScale = 1.0f;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");

    }
   public void QuitGame()
    {
        Application.Quit();
    }
}
