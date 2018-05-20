using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunAlign : MonoBehaviour {
    private redalign red1, red2;
    private LineRenderer timeline;
    private float speed;
    private float screenh, screenw;
    private float roundstart;
    public Text scoretext;
    private int score;

    void Start(){
	screenh = Camera.main.orthographicSize * 2;
	screenw = screenh * Screen.width/ Screen.height;
	red1 = GameObject.Find("red").GetComponent<redalign>();
	red2 = GameObject.Find("red2").GetComponent<redalign>();
	timeline = GameObject.Find("timeline").GetComponent<LineRenderer>();
	timeline.useWorldSpace = true;

	timeline.SetPosition(0, new Vector3(-screenw/2, screenh/2 - timeline.startWidth/2, 0));

	scoretext = GameObject.Find("scoretext").GetComponent<Text>();
	scoretext.text = "0";

	score = 0;

	// speed is the time that the current game lasts in millis
	// for the first round it is 4 seconds
	speed = 4000.0f;
	roundstart = Time.time * 1000;
    }

    void Update(){
	red1.move();
	red2.move();
	float df = Time.time*1000 - roundstart;

	timeline.SetPosition(1, new Vector3(screenw/2 - screenw*(df/speed), screenh/2 - timeline.startWidth/2, 0));

	if (red1.stuckp && red2.stuckp) {
	    reset();
	} else if(df > speed || red1.collidesWithObstaclep() || red2.collidesWithObstaclep()) {
	    //lose
	    SceneManager.LoadScene("lose");
	    Manager.Instance.lastscore = score;
	}
    }

    void reset() {
	score += 1;
	scoretext.text = ""+score;
	speed = (4.7f-Mathf.Pow(score+1.0f, 1.0f/3.0f)*0.8f)*1000.0f;
	roundstart = Time.time * 1000;
	red1.teleportRandom(score);
	red2.teleportRandom(score);
    }
}
