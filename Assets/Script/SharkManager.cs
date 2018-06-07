using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkManager : MonoBehaviour {

    private SliderJoint2D shark;
    private JointMotor2D aux;

	// Use this for initialization
	void Start () {
        shark = GetComponent<SliderJoint2D>();
        aux = shark.motor;
	}
	
	// Update is called once per frame
	void Update () {
        if (shark.limitState == JointLimitState2D.UpperLimit)
        {
            aux.motorSpeed = Random.Range(-1, -7);
            shark.motor = aux;
        }

        if (shark.limitState == JointLimitState2D.LowerLimit)
        {
            aux.motorSpeed = Random.Range(1, 7);
            shark.motor = aux;
        }
    }
}
