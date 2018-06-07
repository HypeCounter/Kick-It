using UnityEngine;
using UnityEngine.UI;

public class Compra : MonoBehaviour {

    public int bolasIDe;
    public Text btnText;
    private GameObject txtMoedas;
    private Animator falido;
   


    public void CompraBolaBtn() {
        for (int i = 0; i< ShopBalls.instance.ballList.Count; i++)
        {
            if (ShopBalls.instance.ballList[i].bolasID == bolasIDe && !ShopBalls.instance.ballList[i].bought && PlayerPrefs.GetInt("moedasSave") >= ShopBalls.instance.ballList[i].bolasPreco)
            {
                ShopBalls.instance.ballList[i].bought = true;
                UpdateCompraBtn();
                ScoreManager.instance.PerdeMoedas(ShopBalls.instance.ballList[i].bolasPreco);
                GameObject.Find("MoedasTXT").GetComponent<Text>().text = PlayerPrefs.GetInt("moedasSave").ToString();                
            }
            else if (ShopBalls.instance.ballList[i].bolasID == bolasIDe && !ShopBalls.instance.ballList[i].bought && PlayerPrefs.GetInt("moedasSave") < ShopBalls.instance.ballList[i].bolasPreco)
            {
                falido = GameObject.FindGameObjectWithTag("falido").GetComponent<Animator>();
                falido.Play("falidoanim");
            }
            else if(ShopBalls.instance.ballList[i].bolasID == bolasIDe && ShopBalls.instance.ballList[i].bought)
            {
                UpdateCompraBtn();
            }
        }
        ShopBalls.instance.UpdateSprite(bolasIDe);
    }
 
    void UpdateCompraBtn()
    {
        btnText.text = "Using it";

        for (int i = 0; i < ShopBalls.instance.compraBtnList.Count; i++)
        {
            Compra compraBolaScript = ShopBalls.instance.compraBtnList[i].GetComponent<Compra>();

            for (int j = 0; j < ShopBalls.instance.ballList.Count; j++)
            {

                if (ShopBalls.instance.ballList[j].bolasID == compraBolaScript.bolasIDe)
                {
                    ShopBalls.instance.SalvaBolaLojaText(compraBolaScript.bolasIDe, "Using it!!");
                    if (ShopBalls.instance.ballList[j].bolasID == compraBolaScript.bolasIDe && ShopBalls.instance.ballList[j].bought && ShopBalls.instance.ballList[j].bolasID == bolasIDe)
                    {
                        LevelLocation.instance.bolaEmUso = compraBolaScript.bolasIDe;
                        PlayerPrefs.SetInt("BolaUse",compraBolaScript.bolasIDe);
                    }
                }

                if (ShopBalls.instance.ballList[j].bolasID == compraBolaScript.bolasIDe && ShopBalls.instance.ballList[j].bought && ShopBalls.instance.ballList[j].bolasID != bolasIDe)
                {
                    compraBolaScript.btnText.text = "Use";
                    ShopBalls.instance.SalvaBolaLojaText(compraBolaScript.bolasIDe, "Use");
                }
            }
        }
    }
    public void FalidoSaida()
    {
        falido = GameObject.FindGameObjectWithTag("falido").GetComponent<Animator>();
        falido.Play("falidoanim_invert");
    }

    public void BuyCoins()
    {
        IAP.Instance.BuyCoins();
    }


  
}
