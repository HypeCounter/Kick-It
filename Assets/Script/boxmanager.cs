using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxmanager : MonoBehaviour {

    [SerializeField] public BombManager estora;
    [SerializeField] private GameObject boxFX;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
		if (estora.estorou == true)
        {
            Instantiate(boxFX, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
	}
}
