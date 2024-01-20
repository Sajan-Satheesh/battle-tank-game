using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private Slider loader;
    [SerializeField] private TMP_Text loadingPercent;
    float loadingProgress = 0;
    private void Awake()
    {
        loader.gameObject.SetActive(false);
    }

    public void playLevel()
    {
        Debug.Log(Application.backgroundLoadingPriority);
        Application.backgroundLoadingPriority = ThreadPriority.High;
        StartCoroutine(loadPlaylevel());
    }

    private IEnumerator loadPlaylevel()
    {
        AsyncOperation playLevelProgress = SceneManager.LoadSceneAsync(1);
        while (!playLevelProgress.isDone)
        {
            loadingProgress = playLevelProgress.progress / 0.9f;
            loader.value = loadingProgress;
            loadingPercent.text = (loadingProgress * 100).ToString();
            Debug.Log("Loading progress: " + loadingProgress);
            yield return null;
        }
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        Application.backgroundLoadingPriority = ThreadPriority.Low;
    }
}
