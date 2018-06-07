using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManagerLevels : MonoBehaviour {

    [SerializeField] Text levelCoins;



	// Use this for initialization
	void Update () {
        ScoreManager.instance.UpdateScore();
        levelCoins.text = PlayerPrefs.GetInt("moedasSave").ToString();
	}
	
	
}
