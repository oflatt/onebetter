using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redalign : MonoBehaviour {
    public Vector2 pos;
    private float screenh, screenw;
    private Vector2 posoffset;
    private Vector2 lp;   //Last touch position
    private float sensativity;
    private GameObject target;
    private float xbound, ybound, wbound, hbound;
    private CircleArcRender arcrenderer;
    private float arcstart, arclength;
    private float redradius;
    public bool stuckp;
    
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

	teleportRandom(0);
    }
	
    // Update is called once per frame
    public void move(){
	if (Input.touches.Length > 0 && !stuckp)
        {
	    for(int i = 0; i<Input.touches.Length; i++){
		//record the touch location
		Touch touch = Input.GetTouch(i);
		Vector2 posunits = pixeltounits(touch.position);

		if(posunits.x - screenw/2 >= xbound && posunits.x-(screenw/2) <= xbound + wbound && posunits.y < ybound  && posunits.y > ybound-hbound){
		    if (touch.phase == TouchPhase.Began) //check for the first touch
		    {
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
	

	if (collidesWithTargetp()) {
	    stuckp = true;
	}
	
    }

    Vector2 pixeltounits(Vector2 p){
	return new Vector2(p.x*(screenw/Screen.width), p.y*(screenh/Screen.height));
    }

    Vector2 currentPos(){
	return new Vector2(transform.position.x, transform.position.y);
    }

    bool collidesWithTargetp(){
	return (transform.position-target.transform.position).magnitude < redradius*1.5f;
    }

    public bool collidesWithObstaclep() {
	if(arclength <0.0000001){
	    return false;
	}
	
	float rx = transform.position.x - (xbound+wbound/2);
	float ry = transform.position.y;
	// first check if it collides between the two ends
	float redangle = Mathf.Atan(ry/rx);
	if((ry < 0 && rx < 0) || (ry > 0 && rx < 0)) {
	    redangle += Mathf.PI;
	}

	redangle = redangle % (Mathf.PI*2);

	float arcstartradians = arcstart * Mathf.Deg2Rad;
	float arcendradians = arcstartradians + arclength*Mathf.Deg2Rad;
	Vector2 rvector = new Vector2(rx, ry);
	float redmagnitude = rvector.magnitude;

	bool collidesbetween = false;

	if(redangle>=arcstartradians && redangle<=arcendradians){
	    collidesbetween = true;
	} else if(arcendradians>Mathf.PI*2){
	    if(redangle <= arcendradians%(Mathf.PI*2)) {
		collidesbetween = true;
	    }
	}
	float circlemag = arcrenderer.xradius;
	
	//check magnitude
	if(collidesbetween) {
	    if (!(redmagnitude <= circlemag+redradius && redmagnitude >= circlemag-redradius)){
		collidesbetween = false;
	    }
	}

	//now check ends of line
	Vector2 end1 = new Vector2(Mathf.Cos(arcstartradians)*circlemag, Mathf.Sin(arcstartradians)*circlemag);
	Vector2 end2 = new Vector2(Mathf.Cos(arcendradians)*circlemag, Mathf.Sin(arcendradians)*circlemag);
	if((end1-rvector).magnitude <= redradius || (end2-rvector).magnitude <= redradius){
	    collidesbetween = true;
	}
	
	
	//if(gameObject.name == "red"){
	//    Debug.Log("redangle: " + redangle);
	//    Debug.Log(arcstartradians);
	//    Debug.Log(arcendradians);
	//}
	return collidesbetween;
    }

    public void teleportRandom(int score){
	// first choose a point using polar coordinates, then convert.
	int newangle = Random.Range(0, 360);
	float spawnradius = (1.0f/4.0f);
	float minmagnitude = spawnradius*wbound + redradius;
	float maxmagnitude = (1.0f/2.0f)*wbound - redradius;
	float magnitude = Random.Range(minmagnitude, maxmagnitude);
	//Debug.Log(magnitude);
	transform.position = new Vector3(Mathf.Cos (Mathf.Deg2Rad *newangle)*magnitude, Mathf.Sin(Mathf.Deg2Rad *newangle)*magnitude, 0) + target.transform.position;

	arcstart = Random.Range(0, 360);
	if(score < 2){
	    arclength = 0;
	} else{
	    float maxarclength = 0.3f*Mathf.Sqrt(score + 6.5f - 2.0f) - 0.6f - 0.0175f*score;
	    float minarclength = maxarclength - 0.1f;
	    maxarclength *= 360;
	    minarclength *= 360;
	    arclength = Random.Range(minarclength, maxarclength);
	}
	arcrenderer.CreatePoints(arcstart, arclength);
	stuckp = false;
    }
}
