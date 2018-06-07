using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallTouch : MonoBehaviour {

    
    public float zRotate;
    public bool liberaRot = false;
    [SerializeField]public bool liberaChute = false;

    BallManager aplicarShoot;

    public static float valorx;
    public static float valory;


    [SerializeField]public static bool ociosa;
    [SerializeField] float fillArrow = .5f;
    [SerializeField] float rotacao = 4f;
   
    
    [SerializeField] static public Rigidbody2D bola;
      
  
   

    public float force = 0;
    
    private Transform rightWall, leftWall;
    int winCount;
    bool destruida = false;
    //  public Collider2D toqueCol;

    public static BallTouch instance;
    [SerializeField] GameObject ballDeath;
    // Use this for initialization
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

        
        
    }
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

     
        
       
        Rotation();
        InputdeRotacao();
        LimitaRotacao();
        PosicionaSeta();
        
        ControlaForca();
     //   Walls();
        if (GameManager.instance.win == true)
        {
            winCount++;

        }
        if (winCount == 1)
        {
            GoalSound();
        }
       
    }
    void PosicionaSeta()
    {
        BallManager.setaGo.GetComponent<Image>().rectTransform.position = BallManager.bolaT.transform.position;
    }

    void Rotation()
    {
        BallManager.setaGo.GetComponent<Image>().rectTransform.eulerAngles = new Vector3(0, 0, zRotate);

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
                BallManager.seta2Img.GetComponent<Image>().fillAmount += fillArrow * Time.deltaTime;
                force = BallManager.seta2Img.GetComponent<Image>().fillAmount * 1000;
            }
            if (moveX > 0)
            {
                BallManager.seta2Img.GetComponent<Image>().fillAmount -= fillArrow * Time.deltaTime;
                force = BallManager.seta2Img.GetComponent<Image>().fillAmount * 1000;

            }

        }
    }
    

    void GoalSound()
    {
        if (GameManager.instance.win == true)
        {
            AudioManager.instance.SoundFxPlay(3);
        }
    }

    private void OnEnable()
    {
        // Subcribe to events when object is enabled
        TouchManager.OnTouchDown += OnTouchDown;
        TouchManager.OnTouchUp += OnTouchUp;
        TouchManager.OnTouchDrag += OnTouchDrag;
    }

    private void OnDisable()
    {
        // Unsubcribe from events when object is disabled
        TouchManager.OnTouchDown -= OnTouchDown;
        TouchManager.OnTouchUp -= OnTouchUp;
        TouchManager.OnTouchDrag -= OnTouchDrag;
    }

    private void OnTouchDown(Touch eventData)
    {

        if (GameManager.instance.tiro == 0)
        {
            liberaRot = true;
            BallManager.setaGo.GetComponent<Image>().enabled = true;
            BallManager.seta2Img.GetComponent<Image>().enabled = true;
         
        }
        
    }

    private void OnTouchUp(Touch eventData)
    {

        liberaRot = false;
        BallManager.setaGo.GetComponent<Image>().enabled = false;
        BallManager.seta2Img.GetComponent<Image>().enabled = false;


        if (GameManager.instance.tiro == 0 && force > 0)
        {
            liberaChute = true;
            BallManager.seta2Img.GetComponent<Image>().fillAmount = 0;
            AudioManager.instance.SoundFxPlay(1);
            GameManager.instance.tiro = 1;
            if (GameManager.instance.ballInScene == 1)
            {
                ociosa = true;
            }
          



        }
    }

    
    private void OnTouchDrag(Touch eventData)
    {
        
    }

}
