using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFXBallDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(KillFx());
	}
	
	IEnumerator KillFx()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
