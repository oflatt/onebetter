﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redalign : MonoBehaviour {
    public Rigidbody2D rb;
    public Vector2 pos;
    
    // Use this for initialization
    void Start () {
	rb = GetComponent<Rigidbody2D>();
	//rb.velocity = new Vector2(1, rb.velocity.y);
        pos = new Vector2(0,0);
	transform.position = pos;
    }
	
    // Update is called once per frame
    void Update () {	
	teleportRandom();
    }

    void teleportRandom() {
	if(Random.Range(-10,90) == 0){
	    transform.position = new Vector2(Random.Range(-10f, 10f), Random.Range(-5f, 5f));
	}
    }
}
