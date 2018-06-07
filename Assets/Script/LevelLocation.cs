using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLocation : MonoBehaviour {


    public static LevelLocation instance;
    public int level = -1;
    [SerializeField]private GameObject uiManager, gameManager;
    public int bolaEmUso;
    private float orthoSize = 5;
    [SerializeField] private float aspect = 1.75f;
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
        SceneManager.sceneLoaded += LevelCheck;
        bolaEmUso = PlayerPrefs.GetInt("BolaUse");
    }

    void LevelCheck(Scene cena, LoadSceneMode modo)
    {
        level = SceneManager.GetActiveScene().buildIndex;
        if (level != 0 && level != 1 && level != 2)
        {
         
            Instantiate(uiManager);
            Instantiate(gameManager);
            Camera.main.projectionMatrix = Matrix4x4.Ortho(-orthoSize* aspect, orthoSize * aspect, -orthoSize, orthoSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
        }
    
    }


}
