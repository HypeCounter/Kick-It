using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBalls : MonoBehaviour {

    public static ShopBalls instance;
    public List<SpecialBalls> ballList = new List<SpecialBalls>();
    public GameObject baseBallItem;
    public List<GameObject> compraBtnList = new List<GameObject>();
    public Transform content;
    public List<GameObject> bolaSuporteList = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
        { instance = this; }
    }
    // Use this for initialization
    void Start () {
        
        FillList();
        //PlayerPrefs.DeleteAll();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FillList()
    {
        foreach (SpecialBalls b in ballList)
        {
            GameObject itensBall = Instantiate(baseBallItem) as GameObject;
            itensBall.transform.SetParent(content, false);
            BallsSupport item = itensBall.GetComponent<BallsSupport>();
            item.bolaID = b.bolasID;
            item.bolaPreco.text = b.bolasPreco.ToString();
           // item.bolaPrecoIAP.text = b.bolaPrecoIAP.ToString();
            item.btnCompra.GetComponent<Compra>().bolasIDe = b.bolasID;
            //lista compra btn
            compraBtnList.Add(item.btnCompra);


            // lista bola suporte

            bolaSuporteList.Add(itensBall);

            if (PlayerPrefs.GetInt("BTN" + item.bolaID) ==1 )
            {
                b.bought = true;
            }

            if (PlayerPrefs.HasKey("BTNS"+item.bolaID)&& b.bought)
            {              
                item.btnCompra.GetComponent<Compra>().btnText.text = PlayerPrefs.GetString("BTNS" + item.bolaID);
            }

            if (b.bought == true)
            {
                item.spriteBall.sprite = Resources.Load<Sprite>("Sprites/" + b.nameBall);
                item.bolaPreco.text = " Got it!!!!";
                if(PlayerPrefs.HasKey("BTNS"+item.bolaID)== false)
                {
                    item.btnCompra.GetComponent<Compra>().btnText.text = "Using";
                }

            }
            else
            {
               item.spriteBall.sprite = Resources.Load<Sprite>("Sprites/" + b.nameBall+ "_cinza");
                
            }
        }
    }

    public void UpdateSprite(int bola_id)
    {
        for(int i = 0; i < bolaSuporteList.Count; i++)
        {
            BallsSupport bolasSuportScript = bolaSuporteList[i].GetComponent<BallsSupport>();
            if (bolasSuportScript.bolaID == bola_id)
            {
                for(int j =0; j< ballList.Count;j++)
                {
                    if(ballList[j].bolasID == bola_id)
                    {
                        if(ballList[j].bought == true)
                        {                            
                            bolasSuportScript.spriteBall.sprite = Resources.Load<Sprite>("Sprites/" + ballList[j].nameBall);
                            bolasSuportScript.bolaPreco.text = "Got it!!!!";
                            SalvaBolasLojaInfo(bolasSuportScript.bolaID);
                        }
                        else
                        {
                            bolasSuportScript.spriteBall.sprite = Resources.Load<Sprite>("Sprites/" + ballList[j].nameBall+"_cinza");
                        }
                    }
                }
            }
        }
    }

    void SalvaBolasLojaInfo(int idBola)
    {
        for (int i = 0; i < ballList.Count; i++)
        {
            BallsSupport bolasSup = bolaSuporteList[i].GetComponent<BallsSupport>();
            if (bolasSup.bolaID == idBola)
            {
                PlayerPrefs.SetInt("BTN"+bolasSup.bolaID,bolasSup.btnCompra ? 1 : 0);
            }

        }
    }
    public void SalvaBolaLojaText(int idBola, string s)
    {

        for (int i = 0; i < ballList.Count; i++)
        {
            BallsSupport bolaSup = bolaSuporteList[i].GetComponent<BallsSupport>();
            if(bolaSup.bolaID == idBola)
            {
                PlayerPrefs.SetString("BTNS"+bolaSup.bolaID, s);
            }
        }
    }
}
