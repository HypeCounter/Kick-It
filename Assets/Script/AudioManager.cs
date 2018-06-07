using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    //musicas
    public AudioClip[] clips;
    public AudioSource bgMusic;
    public static AudioManager instance;

    //sound FX
    public AudioClip[] clipsFX;
    public AudioSource soundsFX;
  

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

    AudioClip GetRandom()
    {
        
        return clips[Random.Range(0, clips.Length)];
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!bgMusic.isPlaying)
        {
            bgMusic.clip = GetRandom();           
            bgMusic.Play();       
            
        }

	}
    public void SoundFxPlay(int index)
    {
        soundsFX.clip = clipsFX[index];
        soundsFX.Play();
    }
}
