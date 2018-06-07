using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
public class UIManager : MonoBehaviour {

   
    public static UIManager instance;
    private Text uiPoints, uiBalls;
    [SerializeField]  GameObject losePanel, winPanel, pausePanel;
    //pause
    [SerializeField]  Button pauseBtn,unpauseBtn,levelPauseBtn;
    //lose
    [SerializeField] Button replayBtnLose, levelBtnLose, plusOneBtnLose;
    //win
    [SerializeField] Button replayBtnWin, levelBtnWin, advanceBtnWin, videoWin;
    //moedas

    public int numCoinsBefore, numCoinsAfter, coinsResult;

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
        SceneManager.sceneLoaded += LoadSetup;
        PanelTurnOnOff();
        GetData();
    }

    void LoadSetup(Scene scene, LoadSceneMode modo)
    {
        GetData();
    }
 

    private void GetData()
    {
        if (LevelLocation.instance.level != 1 && LevelLocation.instance.level != 27)
        {
            uiPoints = GameObject.Find("PontosUI").GetComponent<Text>();
            uiBalls = GameObject.Find("UiBalls").GetComponent<Text>();
            losePanel = GameObject.Find("LosePanel");
            winPanel = GameObject.Find("WinPanel");
            pausePanel = GameObject.Find("PausePanel");
            pauseBtn = GameObject.Find("Pause").GetComponent<Button>();
            unpauseBtn = GameObject.Find("Unpause").GetComponent<Button>();
            replayBtnLose = GameObject.Find("ReplayLose").GetComponent<Button>();
            levelBtnLose = GameObject.Find("MenuLose").GetComponent<Button>();
            levelPauseBtn = GameObject.Find("MenuPause").GetComponent<Button>();
            levelBtnWin = GameObject.Find("ReturnMenuWin").GetComponent<Button>();
            advanceBtnWin = GameObject.Find("Advance").GetComponent<Button>();
            replayBtnWin = GameObject.Find("ReplayWin").GetComponent<Button>();
            plusOneBtnLose = GameObject.Find("Video").GetComponent<Button>();
            videoWin = GameObject.Find("VideoWin").GetComponent<Button>();
            pauseBtn.onClick.AddListener(Pause);
            unpauseBtn.onClick.AddListener(UnPause);

            replayBtnLose.onClick.AddListener(ReplayLevel);
            levelBtnLose.onClick.AddListener(Levels);
            levelPauseBtn.onClick.AddListener(Levels);
            levelBtnWin.onClick.AddListener(Levels);
            advanceBtnWin.onClick.AddListener(NextLevel);
            replayBtnWin.onClick.AddListener(ReplayLevel);
            plusOneBtnLose.onClick.AddListener(AdsExtra);
            videoWin.onClick.AddListener(AdsWin);


            numCoinsBefore = PlayerPrefs.GetInt("moedasSave");



        }
    }

    public void StartUI()
    {
        PanelTurnOnOff();
    }

    public void GameOverUI()
    {
        losePanel.SetActive(true);
    }

    public void WinGameUI()
    {
        winPanel.SetActive(true);
    }

    IEnumerator tempo()
    {
        yield return new WaitForSeconds(0.001f);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

  
    void Pause()
    {
     
        pausePanel.SetActive(true);
        Time.timeScale = 0;
      
    }
    void UnPause()
    {        
        Time.timeScale = 1;
        StartCoroutine(WaitPause());

    }
    IEnumerator WaitPause()
    {
        yield return new WaitForSeconds(0.1f);
        pausePanel.SetActive(false);
    }



    void PanelTurnOnOff()
    {
        StartCoroutine(tempo());
    }

    public void UpdateUI()
    {
        uiBalls.text = GameManager.instance.numBall.ToString();
        uiPoints.text = ScoreManager.instance.moedas.ToString();
        numCoinsAfter = ScoreManager.instance.moedas;
    }

    void ReplayLevel()
    {
        if (GameManager.instance.win == false)
        {
            SceneManager.LoadScene(LevelLocation.instance.level);
            coinsResult = numCoinsAfter - numCoinsBefore;
            ScoreManager.instance.PerdeMoedas(coinsResult);
            coinsResult = 0;
        }
        else {
            SceneManager.LoadScene(LevelLocation.instance.level);
            coinsResult = 0;
        }
    }
    void Levels()
    {
        if (GameManager.instance.win == false)
        {
            coinsResult = numCoinsAfter - numCoinsBefore;
            ScoreManager.instance.PerdeMoedas(coinsResult);
            coinsResult = 0;
            SceneManager.LoadScene(1);
        }
        else
        {
            coinsResult = 0;
            SceneManager.LoadScene(1);
        }
    }
    void NextLevel()
    {
        if (GameManager.instance.win == true)
        {
            int temLevelLoc = LevelLocation.instance.level + 1;
            SceneManager.LoadScene(temLevelLoc);
        }
    }
    public void AdsExtra()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = AdsAnalise });

        }

    }

    public void AdsWin()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = WinAdsAnalise });

        }

    }

    void WinAdsAnalise(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            AudioManager.instance.SoundFxPlay(0);
            ScoreManager.instance.ColetaMoedas(200);
        }
    }



    void AdsAnalise(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            losePanel.SetActive(false);
            GameManager.instance.numBall = 1;
        }
    }


}
