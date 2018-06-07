using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsControl : MonoBehaviour {

    [SerializeField] int coinValor = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("ball"))
        {
            ScoreManager.instance.ColetaMoedas(coinValor);
            AudioManager.instance.SoundFxPlay(0);
            Destroy(this.gameObject);
        }
    }
}
