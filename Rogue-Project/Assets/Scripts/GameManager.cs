using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState { PAUSE, GAMEPLAY, OPTIONS }


public class GameManager : MonoBehaviour
{
    private AsyncOperation async;
    public void StartBtn(int i)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(1);
            async.allowSceneActivation = true;
        }
    }

    [Header("Game State")]
    public GameState currentState;


    void Start()
    {

    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseGame();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void pauseGame()
    {
        bool pauseState = PausePannel.activeSelf;
        pauseState = !pauseState;
        PausePannel.SetActive(!PausePannel.activeSelf);

        switch (pauseState)
        {
            case true:
                Time.timeScale = 0;
                changeState(GameState.PAUSE);
                break;
            case false:
                Time.timeScale = 1;
                changeState(GameState.GAMEPLAY);
                break;
        }

    }
    
    public void optionsButton()
    {
        PausePannel.SetActive(false);
        OptionsPannel.SetActive(true);
        changeState(GameState.OPTIONS);
    }
    public void closePannel()
    {
        OptionsPannel.SetActive(false);
        PausePannel.SetActive(true);
        changeState(GameState.PAUSE);
    }

    public void Exit()
    {
#if UNITY_EDITOR        
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void changeState(GameState newState)
    {
        currentState = newState;
    }

    [Header("Pannels")]
    public GameObject PausePannel;
    public GameObject OptionsPannel;

    [Header("Levels")]
    public GameObject Level1;
           
}
