using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lose : MonoBehaviour
{
    private Button retrybutton;
    private Text scoretext; 

    void Start() {
	Button retrybutton = GameObject.Find("retrybutton").GetComponent<Button>();
	retrybutton.onClick.AddListener(onRetryClick);
	scoretext = GameObject.Find("score").GetComponent<Text>();
	scoretext.text = "" + Manager.Instance.lastscore;
    }

    void onRetryClick() {
	SceneManager.LoadScene("align");
    }
}
