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
    private CircleArcRender arcrenderer;
    private float arcstart, arclength;
    private float redradius;
    
    // Use this for initialization
    void Start () {
	screenh = Camera.main.orthographicSize * 2;
	screenw = screenh * Screen.width/ Screen.height;

	float widthofred = GetComponent<SpriteRenderer>().bounds.size.x;
	redradius = widthofred/2.0f;
	posoffset = new Vector2(redradius, redradius); 
       
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

	arcrenderer = (CircleArcRender) target.GetComponent(typeof(CircleArcRender));
	arcrenderer.xradius = wbound*(1.0f/6.0f);
	arcrenderer.yradius = arcrenderer.xradius;
	
	teleportRandom();
    }
	
    // Update is called once per frame
    public void move(){	
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
	if((transform.position-target.transform.position).magnitude < redradius*1.5f){
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
	// first choose a point using polar coordinates, then convert.
	int newangle = Random.Range(0, 360);
	float spawnradius = (1.0f/4.0f);
	float minmagnitude = spawnradius*wbound + redradius;
	float maxmagnitude = (1.0f/2.0f)*wbound - redradius;
	float magnitude = Random.Range(minmagnitude, maxmagnitude);
	//Debug.Log(magnitude);
	transform.position = new Vector3(Mathf.Sin (Mathf.Deg2Rad *newangle)*magnitude, Mathf.Cos(Mathf.Deg2Rad *newangle)*magnitude, 0) + target.transform.position;
	
	arcstart = Random.Range(0, 360);
	arclength = Random.Range(30, 180);
	arcrenderer.CreatePoints(arcstart, arclength);
    }
}
