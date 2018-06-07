using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doggy : MonoBehaviour
{
    public bool face = true;
    public Transform doggyT;
    public GameObject ball = null;
    [SerializeField] float vel = .5f;
    [SerializeField] Rigidbody2D doggyRB;
    public Transform target;
    public float speed = 2f;
    [SerializeField] private float minDistance = 0.5f;
    [SerializeField] private float range;
    public Animator anim;
    AudioSource audioSource;


    void Start()
    {
        doggyT = GetComponent<Transform>();
        doggyRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
     

    }





    void Update()
    {
        ball = GameObject.FindGameObjectWithTag("ball");
        target = ball.GetComponent<Transform>();
        range = Vector2.Distance(transform.position, target.position);

        if (range <= minDistance)
        {
            audioSource.volume = 0.7f; 
           
            if (ball.transform.position.x < transform.position.x)
            {

                FlipLeft();

            }
            else
            {
                FlipRight();
            }
            if (ball.transform.position.y <= transform.position.y)
            {
                anim.SetBool("Andar", true);
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                

            }



        }
        else
        {
            audioSource.volume = 0.05f;
            anim.SetBool("Andar", false);
        }
    }
    void FlipLeft()
    {

        
        Vector3 scala = doggyT.localScale;
        scala.x = -0.7f;
        doggyT.localScale = scala;
    }
    void FlipRight()
    {
        Vector3 scala = doggyT.localScale;
        scala.x = 0.7f;
        doggyT.localScale = scala;
    }
}
