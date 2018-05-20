using UnityEngine;

public class Manager : MonoBehaviour{
    public static Manager Instance {get; set;}

    public int lastscore;

    private void Awake(){
	if (Instance == null) {
	    Instance = this;
	    DontDestroyOnLoad(gameObject);
	}
	else{
	    Destroy(gameObject);
	}
    }
}
