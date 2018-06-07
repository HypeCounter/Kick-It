using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour {
          
    public GameObject setaGo;
    public float zRotate;
    public bool liberaRot = false;
    public bool liberaChute = false;

    [SerializeField] float fillArrow = .5f;
    [SerializeField] float rotacao = 4f;

    private Rigidbody2D bola;
    private float force = 0;
    public GameObject seta2Img;
    private Transform rightWall, leftWall;
    int winCount;
    public Collider2D toqueCol;
   

    [SerializeField] GameObject ballDeath;
    // Use this for initialization
    void Awake()
    {
        rightWall = GameObject.Find("RightWall").GetComponent<Transform>();
        leftWall = GameObject.Find("LeftWall").GetComponent<Transform>();
        setaGo = GameObject.Find("Seta");
        seta2Img = setaGo.transform.GetChild(0).gameObject;
        setaGo.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;
    }
    void Start () {


        bola = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody2D>();
       

    }

    // Update is called once per frame
    void Update () {
        Rotation();
        InputdeRotacao();
        LimitaRotacao();
        PosicionaSeta();
        AplicaForca();
        ControlaForca();
        Walls();
        if (GameManager.instance.win == true)
        {
            winCount++;
             
        }
        if (winCount == 1) {
            GoalSound();
        }
        if (GameManager.instance.win == true)
        {
            StopAllCoroutines();
        }
    }
    void PosicionaSeta()
    {
        setaGo.GetComponent<Image>().rectTransform.position = transform.position;
    }  

    void Rotation()
    {
        setaGo.GetComponent<Image>().rectTransform.eulerAngles = new Vector3(0, 0, zRotate);

    }

    void InputdeRotacao()
    {
        if (liberaRot == true)
        {
            float moveY = Input.GetAxis("Mouse Y");
            if (Input.touchCount > 0)
            {             
                moveY = Input.touches[0].deltaPosition.y;
            }
            if (zRotate < 90)
            {
                if (moveY < 0)
                {
                    zRotate += rotacao;

                }
            }

            if (zRotate > 0)
            {

                if (moveY > 0)
                {
                    zRotate -= rotacao;
                }
            }
        }
    }

    void LimitaRotacao()
    {
        if (zRotate >= 90)
        {
            zRotate = 90;
        }

        if (zRotate <= 0)
        {

            zRotate = 0;
        }
    }
    void OnMouseDown()
    {
        if (GameManager.instance.tiro == 0)
        {
            
            liberaRot = true;           
            setaGo.GetComponent<Image>().enabled = true;
            seta2Img.GetComponent<Image>().enabled = true;
            toqueCol = GameObject.FindGameObjectWithTag("Respawn").GetComponentInChildren<Collider2D>();


         
 
        }


    }
    void OnMouseUp()
    {
        liberaRot = false;
        setaGo.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;
               
    
        if (GameManager.instance.tiro == 0 && force >0)
        {
            liberaChute = true;
            seta2Img.GetComponent<Image>().fillAmount =0;
            AudioManager.instance.SoundFxPlay(1);
            GameManager.instance.tiro = 1;
            toqueCol.enabled = false;
            StartCoroutine(BolaOciosa());
            

        }
        
    }
    void AplicaForca()
    {

        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);

        if (liberaChute == true)
        {
            bola.AddForce(new Vector2(x, y));
            liberaChute = false;
        }


    }
    void ControlaForca()
    {
        if (liberaRot == true)
        {
            float moveX = Input.GetAxis("Mouse X");
            if (Input.touchCount > 0)
            {
                moveX = Input.touches[0].deltaPosition.x;                
            }

            if (moveX < 0)
            {
                seta2Img.GetComponent<Image>().fillAmount += fillArrow * Time.deltaTime;
                force = seta2Img.GetComponent<Image>(). fillAmount * 1000;
            }
            if (moveX > 0)
            {
                seta2Img.GetComponent<Image>().fillAmount -= fillArrow * Time.deltaTime;
                force = seta2Img.GetComponent<Image>().fillAmount * 1000;

            }

        }
    }
    void DynamicBall()
    {
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }
    void Walls()
    {
        if(this.gameObject.transform.position.x > rightWall.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.ballInScene -= 1;
            GameManager.instance.numBall -= 1;
        }
        if (this.gameObject.transform.position.x < leftWall.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.ballInScene -= 1;
            GameManager.instance.numBall -= 1;
        }
    }
    IEnumerator BolaOciosa()
    {

        if (bola.velocity == new Vector2(0, 0))
        {
            
                yield return new WaitForSeconds(8f);
                Destroy(this.gameObject);
                GameManager.instance.ballInScene -= 1;
                GameManager.instance.numBall -= 1;
            
        }
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
            int temp = LevelLocation.instance.level -2;
            temp++;
            PlayerPrefs.SetInt("Level"+temp,1);
        }
    }
    void GoalSound()
    {
        if (GameManager.instance.win == true)
        {
            AudioManager.instance.SoundFxPlay(3);
        }
    }
}
