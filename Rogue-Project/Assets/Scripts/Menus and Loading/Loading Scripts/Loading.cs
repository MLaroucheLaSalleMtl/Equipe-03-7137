using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Script description : 
 * Loads the next level (assets and all objects) in background
 * before switching scene without any hiccups.
 */

public class Loading : MonoBehaviour
{
    //Loading Objects.//
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadingSlider;
    [SerializeField] Text loadingText;
    //[SerializeField] Image loadingIcon;

    //Variable List.//
    private float loadingProgress = 0;

    //public void LoadLevel(int level)
    public void LoadLevel()
    {
        StartCoroutine(LoadAsync());
        //StartCoroutine(LoadAsync(level));
    }

    //Loads the gameobjects and assets in the background.//
    //IEnumerator LoadAsync(int level)
    IEnumerator LoadAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);

        loadingScreen.SetActive(true);

        while (!asyncOperation.isDone)
        {
            loadingProgress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // 0.9f so the progress doesn't end with 0.9 but 1.//

            Debug.Log(loadingProgress);

            LoadingElements();
            yield return null;
        }
    }

    public void LoadingElements()
    {
        //Increments values of the slider and the loading icon.//
        //loadingIcon.fillAmount = loadingProgress;
        loadingSlider.value = loadingProgress;

        //Puts demicals into Percentage.//
        loadingText.text = loadingProgress * 100f + "%";
    }

    ////public void LoadingScene(int sceneIndex)
    ////{
    ////    StartCoroutine(LoadAsync(sceneIndex));

    ////}

    ////IEnumerator LoadAsync (int sceneIndex)
    ////{
    ////    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
    ////    loadingScreen.SetActive(true);
    ////    while (!operation.isDone)
    ////    {
    ////        float progress = Mathf.Clamp01(operation.progress / 0.9f);
    ////        loadingSlider.value = progress;
    ////        loadingText.text = progress *100f + "%";

    ////        yield return null;
    ////    }
    ////}
}