using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redalign : MonoBehaviour {
    public Vector2 pos;
    private float screenh, screenw;
    private Vector2 posoffset;
    private Vector2 fp;   //First touch position
    private Vector2 lp;   //Last touch position
    private float sensativity;
    private GameObject target;
    private float xbound, ybound, wbound, hbound;
    
    // Use this for initialization
    void Start () {
	Screen.orientation = ScreenOrientation.LandscapeLeft;

	screenh = Camera.main.orthographicSize * 2;
	screenw = screenh * Screen.height/ Screen.width;
	float widthofred = GetComponent<SpriteRenderer>().bounds.size.x;
	posoffset = new Vector2(widthofred/2, widthofred/2); 
	
        pos = new Vector2(-screenw/2,0) + posoffset;
	transform.position = pos;
	sensativity = 2.0f;

	if(gameObject.name == "red"){
	    target = GameObject.Find("target");
	    xbound = -screenw/2;
	    ybound = screenh/2;
	} else {
	    target = GameObject.Find("target2");
	    xbound = 0;
	    ybound = screenh/2;
	}
	wbound = screenw/2;
	hbound = screenh;
	target.transform.position = new Vector2(target.transform.position.x + xbound + wbound/2, target.transform.position.y);
    }
	
    // Update is called once per frame
    void Update () {	
	if (Input.touches.Length > 0)
        {
	    for(int i = 0; i<Input.touches.Length; i++){
		//record the touch location
		Touch touch = Input.GetTouch(i);
		Vector2 posunits = pixeltounits(touch.position);

		if(posunits.x - screenw/2 >= xbound && posunits.x-(screenw/2) <= xbound + wbound && posunits.y < ybound  && posunits.y > ybound-hbound){
		    if (touch.phase == TouchPhase.Began) //check for the first touch
		    {
			fp = touch.position;
			lp = touch.position;
		    }
		    else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
		    {
			Vector2 delta = pixeltounits(touch.position-lp) * sensativity;
			transform.position = currentPos() + delta;
		
			lp = touch.position;
		    }
		}
	    }
	}

	//collision with edges of screen
	Vector2 bottomleft = currentPos() - posoffset;
	Vector2 topright = currentPos() + posoffset;
	if(bottomleft.x < xbound){
	    transform.position = new Vector2(posoffset.x+xbound, currentPos().y);
	}
	if(topright.x > xbound+wbound){
	    transform.position = new Vector2(xbound+wbound-posoffset.x, currentPos().y);
	}

	if(bottomleft.y<ybound-hbound){
	    transform.position = new Vector2(currentPos().x, posoffset.y+ybound-hbound);
	}
	if(topright.y>ybound){
	    transform.position = new Vector2(currentPos().x, -posoffset.y+ybound);
	}

	
	//collision with target
	if((transform.position-target.transform.position).magnitude < 0.125){
	    teleportRandom();
	}
    }

    Vector2 pixeltounits(Vector2 p){
	return new Vector2(p.x*(screenw/Screen.width), p.y*(screenh/Screen.height));
    }

    Vector2 currentPos(){
	return new Vector2(transform.position.x, transform.position.y);
    }

    void teleportRandom(){
	transform.position = new Vector2(Random.Range(xbound, xbound+wbound), Random.Range(ybound-hbound, ybound)) + posoffset;
    }
}
