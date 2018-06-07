using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallManager : MonoBehaviour {


    [SerializeField] GameObject ballDeath;
    [SerializeField] Rigidbody2D bola;
    public static Transform bolaT;
    public static bool ociosa;
    public static GameObject seta2Img;
    public static GameObject setaGo;
    void Awake()
    {
       // rightWall = GameObject.Find("RightWall").GetComponent<Transform>();
     //   leftWall = GameObject.Find("LeftWall").GetComponent<Transform>();
        setaGo = GameObject.Find("Seta");
        seta2Img = setaGo.transform.GetChild(0).gameObject;
        setaGo.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;
    }
    // Use this for initialization
    void Start () {
        bola = this.GetComponent<Rigidbody2D>();
        bolaT = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        ShoottheBall();
        
        ociosa = BallTouch.ociosa;
        if (ociosa)
        {
            StartCoroutine(BolaOciosa());
        }
        
        if (GameManager.instance.win == true)
        {
            StopAllCoroutines();
        }


    }

    public void ShoottheBall()
    {

       float x = BallTouch.instance.force * Mathf.Cos(BallTouch.instance.zRotate * Mathf.Deg2Rad);
        float y = BallTouch.instance.force * Mathf.Sin(BallTouch.instance.zRotate * Mathf.Deg2Rad);

      
        if (BallTouch.instance.liberaChute == true)
        {

            bola.AddForce(new Vector2(x, y));
            BallTouch.instance.liberaChute = false;
        }
    }

    void DynamicBall()
    {
        bola.GetComponent<Rigidbody2D>().isKinematic = false;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death"))
        {
            Instantiate(ballDeath, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameManager.instance.ballInScene -= 1;
            GameManager.instance.numBall -= 1;
        }
        if (other.gameObject.CompareTag("goal"))
        {
            new WaitForSeconds(1f);

            GameManager.instance.win = true;
            int temp = LevelLocation.instance.level - 2;
            temp++;
            PlayerPrefs.SetInt("Level" + temp, 1);
        }
    }


    
    IEnumerator BolaOciosa()
    {

        if (bola.velocity == new Vector2(0, 0))
        {

            yield return new WaitForSeconds(8f);
            Destroy(gameObject);

            GameManager.instance.ballInScene -= 1;
            GameManager.instance.numBall -= 1;

        }
    }

  //  void Walls()
   // {
   //     if (bolaT.transform.position.x > rightWall.position.x)
      //  {

       //     Destroy(bolaGO.gameObject);
        //    GameManager.instance.ballInScene -= 1;
         //   GameManager.instance.numBall -= 1;
     //       destruida = true;
     //   }
    //    if (bolaT.transform.position.x < leftWall.position.x)
       // {

      //      Destroy(bolaGO.gameObject);
      //      GameManager.instance.ballInScene -= 1;
      //      GameManager.instance.numBall -= 1;
       //     destruida = true;
     //   }
//
  //  }

  //  void OnTriggerEnter2D(Collider2D other)
  //  {
 //       if (other.CompareTag("Death"))
      //  {
      //      Instantiate(ballDeath, transform.position, Quaternion.identity);
      //      Destroy(this.gameObject);
     //       GameManager.instance.ballInScene -= 1;
      //      GameManager.instance.numBall -= 1;
      ///  }
      //  if (other.gameObject.CompareTag("goal"))
     //   {
     //       new WaitForSeconds(1f);
     //
     //       GameManager.instance.win = true;
     //       int temp = LevelLocation.instance.level - 2;
     //       temp++;
      //      PlayerPrefs.SetInt("Level" + temp, 1);
//}
 //   }


}
