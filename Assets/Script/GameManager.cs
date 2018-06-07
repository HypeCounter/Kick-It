using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
   
    //ball
    [SerializeField] public GameObject[] ball;
    public int numBall = 3;
    private bool ballDeath = false;
    public int ballInScene = 0;
    public Transform myPosition;
    public bool win;
    public int levelLocation;
    public bool startGame;
    public int tiro = 0;

    private bool adsUmaVez = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += LoadGameManager;
        myPosition = GameObject.Find("posStart").GetComponent<Transform>();
        StartGame();
    }
        
    void Start () {
        ScoreManager.instance.GameStartScoreM();      

	}
		
	void Update () {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
        BallInstantiate();
        if (numBall <= 0 && win == false)
        {
            GameOver();
        }
        if (win == true)
        {            
            WinGame();
        }
    }
    public void WinGame()
    {
        UIManager.instance.WinGameUI();
        startGame = false;
    }
    void BallInstantiate()
    {
        if (LevelLocation.instance.level >= 15)
        {
            if (numBall > 0 && ballInScene == 0 && Camera.main.transform.position.x <= 0.05f)
            {
          
                Instantiate(ball[LevelLocation.instance.bolaEmUso], new Vector2(myPosition.position.x, myPosition.position.y), Quaternion.identity);
                ballInScene += 1;
                tiro = 0;
            }
        }
        else
        {
            if (numBall > 0 && ballInScene == 0)
            {
               
                Instantiate(ball[LevelLocation.instance.bolaEmUso], new Vector2(myPosition.position.x, myPosition.position.y), Quaternion.identity);
                ballInScene += 1;
                tiro = 0;
            }
        }
        
    }
    void LoadGameManager(Scene scene, LoadSceneMode modo)
    {
        if (LevelLocation.instance.level != 1 && LevelLocation.instance.level != 27) {
            myPosition = GameObject.Find("posStart").GetComponent<Transform>();           
            StartGame();
        }
    }
    void GameOver()
    {
        UIManager.instance.GameOverUI();
        startGame = false;
        if (adsUmaVez == false)
        {
            AdsUnity.instance.ShowAds();
            adsUmaVez = true;
        }
    }
    void StartGame()
    {
        startGame = true;
        numBall = 3;
        ballInScene = 0;
        win = false;
        UIManager.instance.StartUI();
        adsUmaVez = false;
    }

}


