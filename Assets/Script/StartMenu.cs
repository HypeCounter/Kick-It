using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    private Animator barraAnim;
    private bool up;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void MenuAnimation()
    {
        barraAnim = GameObject.FindGameObjectWithTag("menustart").GetComponent<Animator>();
        if (up == false)
        {
            barraAnim.Play("UI_Move");
            up = true;
        }
        else
        {
            barraAnim.Play("UI_Down");
            up = false;
        }
        
    }
}
