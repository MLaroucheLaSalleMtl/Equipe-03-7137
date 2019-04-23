    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public enum GameState { PAUSE, GAMEPLAY, OPTIONS, FIGHTING, ITEMS }


public class GameManager : MonoBehaviour
{
    //Variable List.//
    public bool isWalking = false;
    public bool CanFight = false; //Prevent from entering a fight twice => enemyVicinity(); //
    public bool isFighting = false;
    
    public int enemyCount = 0;

    //Lists.//

    //il faut creer la liste pour les items et une autre pour les images des items.
    public List<GameObject> NumberOfEnemies = new List<GameObject>(); //Manages the numbers of enemies inside a Fight.//

    public Camera camera;
    public static GameManager gameManager;
    public GameObject MainCharacter;
    public Inventory inventory;

    [SerializeField] private BattleStateMachine battleManager;



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

    [Header("[Game State]")]
    public GameState currentState;


    [Header("[Pannels]")]
    public GameObject PausePannel;
    public GameObject OptionsPannel;
    public GameObject ItemsPannel;
    public GameObject ItemInfoPannel;

    [Header("[Interaction Pannels]")]
    public GameObject BedPannel;

    [Header("[Levels]")]
    public GameObject[] levels = new GameObject[5]; //NOTE :levels[o] == safehouse
    public static int currentLevel = 1;
    public static int previousLevel = 0;

    void Awake()
    {
        CheckGM();
        //battleManager.SetActive(false);
        foreach (var item in levels) // activate all items to get them
        {
            if (!item.activeInHierarchy) 
            {
                item.SetActive(true);
            }
        }
        //getting all the levels
        for (int i = 0; i < levels.Length; i++)
        {
            if (i == 0)
            {
                levels[i] = GameObject.Find("SafeHouse");
                print("found safehouse lvl");
                levels[i].SetActive(false);
            }
            else
            {
                levels[i] = GameObject.Find($"Level{i}");
                print($"found level{i}");
            }

            if (levels[i].activeInHierarchy)
            {
                levels[i].SetActive(false);
            }
        }
        if (!levels[1].activeInHierarchy)
        {
            levels[1].SetActive(true);
        }
        MainCharacter = GameObject.Find("MainCharacter");

        if (!GameObject.Find("MainCharacter"))
        {
            GameObject MC = Instantiate(MainCharacter, Vector3.zero, Quaternion.identity) as GameObject; //Set the MC vector to 0 and same for rotation (Quaternion).//
            MC.name = "Main Character";
        }
        
    }

    void Start()
    {
        inventory = FindObjectOfType(typeof(Inventory)) as Inventory;
        ItemsPannel.SetActive(false);
        inAFight = false;
        ItemInfoPannel.SetActive(false);
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ActivateInventory();
        }
    }

    void ActivateInventory()
    {
        bool pauseState = ItemsPannel.activeSelf;
        pauseState = !pauseState;
        ItemsPannel.SetActive(!ItemsPannel.activeSelf);

        switch (pauseState)
        {
            case false:
                Time.timeScale = 0;
                changeState(GameState.ITEMS);
                break;
            case true:
                Time.timeScale = 1;
                changeState(GameState.GAMEPLAY);
                break;
        }
        btnItems();
        //inventory.loadInventory();
        changeState(GameState.ITEMS);
        //print("hello inventory");
    }

    void Update()
    {

        //bool Idown = Input.GetKey(KeyCode.I);
        if (Input.GetButtonDown("Cancel"))
        {
            pauseGame();
        }
        switch (currentState)
        {
            
            case (GameState.GAMEPLAY):
                OptionsPannel.SetActive(false); 
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
        //if (Idown)
        //{
        //    btnItems();
        //    inventory.loadInventory();
        //    changeState(GameState.ITEMS);
        //    Idown = !Idown;
        //}
    }

    private void vicinityEnemy()
    {
        if (isWalking && CanFight) //Prevent from entering a fight twice.//
        {
            //Enemy is within range.//
            isFighting = true;
        }
    }


       static public bool inAFight = false;
    public void StartFight()
    {
        if (!inAFight)
        {
            battleManager.StartBattle();
            print("Battle started");
        }
        inAFight = true;

    }
    public void BossFight()
    {
        if (!inAFight)
        {
            //Add boss to parameters to start battle with it

            battleManager.StartBossBattle(new List<EnemyBaseClass>() { EnemyBaseClass.Boss()});
            print("Boss Battle Started !");
        }
        inAFight = true;
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
        ItemsPannel.SetActive(false);

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
    
    public void btnItems()
    {
        ItemsPannel.SetActive(true);
        changeState(GameState.ITEMS);
        Time.timeScale = 0;
    }

    public void CloseItemsPannel()
    {
        ItemsPannel.SetActive(false);
        changeState(GameState.GAMEPLAY);
        Time.timeScale = 1;
        //inventory.clearLoadedItems();
    }

    public void openItemInfoPannel()
    {
        ItemInfoPannel.SetActive(true);
    }

    public void closeItemInfoPannel()
    {
        ItemInfoPannel.SetActive(false);
    }

}
