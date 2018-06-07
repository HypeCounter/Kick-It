using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [System.Serializable]
    public class Level
    {
        public string levelText;
        public bool habilitado;
        public int desbloqueado;
      //  public bool txtAtivo;
    }

    public GameObject button;
    public Transform localBtn;
    public List<Level> levelList;

    void AddList()
    {
        foreach(Level level in levelList)
        {
            GameObject newBtn = Instantiate(button) as GameObject;
            LevelButton btnNew = newBtn.GetComponent<LevelButton>();
            btnNew.levelTxtBtn.text = level.levelText;
            if (PlayerPrefs.GetInt("Level" + btnNew.levelTxtBtn.text)== 1)
            {
                level.desbloqueado = 1;
                level.habilitado = true;
                
            }
            btnNew.desbloqueadoBTN = level.desbloqueado;
            btnNew.GetComponent<Button>().interactable = level.habilitado;
            btnNew.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level"+btnNew.levelTxtBtn.text));
         
            newBtn.transform.SetParent(localBtn, false);

        }
    }
    void Awake()
    {
        Destroy(GameObject.Find("UI_Manager(Clone)"));
        Destroy(GameObject.Find("GameManager(Clone)"));
    }


    void Start () {
        AddList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
