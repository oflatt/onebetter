using UnityEngine;
using System.Collections;

public class RunAlign : MonoBehaviour {
    private redalign red1, red2;

    void Start(){
	red1 = GameObject.Find("red").GetComponent<redalign>();
	red2 = GameObject.Find("red2").GetComponent<redalign>();
    }

    void Update(){
	red1.move();
	red2.move();
    }
}
