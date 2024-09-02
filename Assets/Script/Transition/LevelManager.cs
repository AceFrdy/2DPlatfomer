using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Slider progressBar;
    public GameObject transitionsContainer;

    private SceneTransition[] transitions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (transitionsContainer == null)
        {
            Debug.LogError("Transitions container is not assigned.");
            return;
        }

        transitions = transitionsContainer.GetComponentsInChildren<SceneTransition>();

        if (transitions.Length == 0)
        {
            Debug.LogError("No SceneTransition components found in transitionsContainer.");
        }
    }

    public void LoadScene(string sceneName, string transitionName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name cannot be null or empty.");
            return;
        }

        if (string.IsNullOrEmpty(transitionName))
        {
            Debug.LogError("Transition name cannot be null or empty.");
            return;
        }

        StartCoroutine(LoadSceneAsync(sceneName, transitionName));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string transitionName)
    {
        // Memastikan musik berhenti sebelum transisi masuk
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.StopMusic();
        }

        if (transitions == null || transitions.Length == 0)
        {
            Debug.LogError("No SceneTransition components found.");
            yield break;
        }

        SceneTransition transition = transitions.FirstOrDefault(t => t.name == transitionName);

        if (transition == null)
        {
            Debug.LogError($"No SceneTransition found with the name: {transitionName}");
            yield break;
        }

        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        yield return transition.AnimateTransitionIn();

        if (progressBar == null)
        {
            Debug.LogError("Progress bar is not assigned.");
            yield break;
        }

        progressBar.gameObject.SetActive(true);

        do
        {
            progressBar.value = scene.progress;
            yield return null;
        } while (scene.progress < 0.9f);

        yield return new WaitForSeconds(1f);

        scene.allowSceneActivation = true;

        progressBar.gameObject.SetActive(false);

        yield return transition.AnimateTransitionOut();
    }

    public void RestartLevel()
    {
        if (PlayerPrefs.GetInt("HasPlayedCutscene", 0) == 1)
        {
            SkipCutscene();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name cannot be null or empty.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    private void SkipCutscene()
    {
        // Di sini kita bisa melakukan skip cutscene atau logic lain yang diperlukan
        Debug.Log("Skipping Cutscene");
    }
}
