using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAutoDestroy : MonoBehaviour {

    private GameObject bombRep;




	void Start () {

        bombRep = GameObject.Find("Bomb");
        
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
