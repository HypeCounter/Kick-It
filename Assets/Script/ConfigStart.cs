using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigStart : MonoBehaviour {
    private AudioSource volume;
    public Sprite somLigado, somDesligado;
    private Button btnSound;
	// Use this for initialization
	void Start () {
        volume = GameObject.Find("AudioManager").GetComponent<AudioSource>() as AudioSource;
        btnSound = GameObject.Find("SomIMG").GetComponent<Button>() as Button;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnOffSound()
    {
        volume.mute = !volume.mute;
        if (volume.mute == true)
        {
            btnSound.image.sprite = somDesligado;
                }
        else {
            btnSound.image.sprite = somLigado;
        }
    }
    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/Pixel-Strike-2032414970106288/");
    }
}
