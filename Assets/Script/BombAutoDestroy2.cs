using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAutoDestroy2 : MonoBehaviour {

    private GameObject bombRep;




	void Start () {

        bombRep = GameObject.Find("Bomb2");
        
    }
	

	void Update () {
    
        StartCoroutine(Life());
	}

    IEnumerator Life()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(bombRep.gameObject);
        Destroy(this.gameObject);
    }
}
