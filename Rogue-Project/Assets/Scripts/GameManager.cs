using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState { PAUSE, GAMEPLAY, OPTIONS, FIGHTING }


public class GameManager : MonoBehaviour
{
    //Variable List.//
    public bool isWalking = false;
    public bool CanFight = false; //Prevent from entering a fight twice => enemyVicinity(); //
    public bool isFighting = false;

    public int enemyCount = 0;

    //Lists.//
    public List<GameObject> NumberOfEnemies = new List<GameObject>(); //Manages the numbers of enemies inside a Fight.//


    public static GameManager gameManager;
    public GameObject MainCharacter;
    [SerializeField] private GameObject battleManager;

    //Thingy to make battle screen appear, Brad's way thing.

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

    void Awake()
    {
        CheckGM();
        battleManager.SetActive(false);
        if (!GameObject.Find("MC-Standing"))
        {
            GameObject MC = Instantiate(MainCharacter, Vector3.zero, Quaternion.identity) as GameObject; //Set the MC vector to 0 and same for rotation (Quaternion).//
            MC.name = "Main Character";
        }
    }

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseGame();
        }
        switch (currentState)
        {
            case (GameState.GAMEPLAY):

                if (isWalking)
                {
                    vicinityEnemy();
                }
                if (isFighting)
                {
                    currentState = GameState.FIGHTING;
                }

                break;

            case (GameState.FIGHTING):
                //Make shit pop-up.//

                break;
        }
    }

    private void vicinityEnemy()
    {
        if (isWalking && CanFight) //Prevent from entering a fight twice.//
        {
            //Enemy is within range.//
            isFighting = true;
        }
    }
    private void StartFight()
    {
        //Count how many enemies are in the vicinity.//
        for (int i = 0; i < enemyCount; i++)
        {
            //NumberOfEnemies.Add(  //Add enemies within range? // )

        }

        //Popup scene Brad's way.//

        //Player Reset.//
        isWalking = false;
        isFighting = false;
        CanFight = false;
    }
    private void CheckGM() //Check for gameManager object duplication.//
    {
        if (gameManager = this)//If the gameManager exists, if not set it to the GameObject.//
        {
            gameManager = this;
        }
        else if (gameManager != this) //If it already exists, destroy it to prevent duplicates.//
        {
            Destroy(gameObject);
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
        //PausePannel.SetActive(false);
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
