using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public float WorldSpped;
    public float adjustedWorldSpeed;

    public int CritterCount;
     private ObjectPooler Boss1Pool;



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);

        }
        else
            instance = this;
    }
    private void Start()
    {
        CritterCount = 0;
        Boss1Pool = GameObject.Find("Boss1Pool").GetComponent<ObjectPooler>();
    }
    private void Update()
    {
        adjustedWorldSpeed = WorldSpped * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire3")){

            Pausedgame();

        }
        if (CritterCount > 5) {
            CritterCount = 0;
            GameObject boss1 = Boss1Pool.GetPooledObject();
            boss1.transform.position = new Vector2(15f, 0);
            boss1.transform.rotation = Quaternion.Euler(0, 0,-90);
            boss1.SetActive(true);


        }
    }
    public void Pausedgame()
    {
        if (UIController.instance.Paused.activeSelf== false)
        {
            UIController.instance.Paused.SetActive(true);
            Time.timeScale = 0f;
            AudioManager.instance.PlaySound(AudioManager.instance.pause);

        }
        else
        {
            UIController.instance.Paused.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.instance.ExitBoosting();
            AudioManager.instance.PlaySound(AudioManager.instance.unpause);

        }

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GameOver()
    {
        StartCoroutine(ShowGameOver());
    }
    IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }
    public void SetWorldSpeed(float speed)
    {
        WorldSpped = speed;
    }
}
