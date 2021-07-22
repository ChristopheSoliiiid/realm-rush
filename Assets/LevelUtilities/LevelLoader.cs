using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int timeToWait = 3;
    [SerializeField] GameObject LostInterface;

    int currentSceneIndex;
    bool isPlaying = true;
    public bool IsPlaying { get { return isPlaying; } }

    delegate void DelegateMethod();

    void Awake()
    {
        if (LostInterface != null) {
            LostInterface.SetActive(false);
        }
    }

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 0) {
            StartCoroutine(WaitForTime(LoadNextScene));
        }
    }

    void PlayingStatus()
    {
        isPlaying = true;
    }

    void LostStatus()
    {
        isPlaying = false;
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void ReloadLevel()
    {
        PlayingStatus();

        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("Options Screen");
    }

    public void LoadGameOver()
    {
        LostStatus();

        if (LostInterface != null) {
            LostInterface.SetActive(true);
        } else {
            StartCoroutine(WaitForTime(ReloadLevel));
        }       
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitForTime(DelegateMethod methodToCall)
    {
        yield return new WaitForSecondsRealtime(timeToWait);

        PlayingStatus();

        methodToCall();
    }
}
