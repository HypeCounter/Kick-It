using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour {

    [SerializeField] int coinValor = 100;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            ScoreManager.instance.ColetaMoedas(coinValor);            
            AudioManager.instance.SoundFxPlay(2);
            

        }
    }
    
}
