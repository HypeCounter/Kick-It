using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] Transform objE, objD, ballCamera;
    private float t = 1;
    [SerializeField] float valorSuave = .1f;

	// Use this for initialization
	void Start () {

       
    }
	
	// Update is called once per frame
	void Update () {
        
        if (GameManager.instance.startGame == true)
        {
            if (transform.position.x != objE.position.x && GameManager.instance.ballInScene == 0)
            {
              
                t -= valorSuave * Time.deltaTime;
                transform.position = new Vector3(Mathf.SmoothStep(objE.position.x, Camera.main.transform.position.x, t), this.transform.position.y, this.transform.position.z);
            }
            if (ballCamera == null && GameManager.instance.ballInScene >0)
            {

                ballCamera = GameObject.Find(LevelLocation.instance.bolaEmUso +"(Clone)").GetComponent<Transform>();
            }
            else if (GameManager.instance.ballInScene > 0)
            {
                Vector3 posCam = transform.position;
                posCam.x = ballCamera.position.x;
                posCam.x = Mathf.Clamp(posCam.x, objE.position.x, objD.position.x);
                transform.position = posCam;
            }
        }
		
	}
}
