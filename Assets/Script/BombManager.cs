using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour {

    [SerializeField] private GameObject bombFX;
    public bool estorou;
    

	void Start () {
        estorou = false;
    }
	

	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)

    {
        if (other.gameObject.CompareTag("ball"))
        {
            estorou = true;
            Instantiate(bombFX, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
           
        }
    }

}
