using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    
    void OnGUI()
    {
	GUIStyle g = new GUIStyle();
	g.fontSize = 30;
	Vector2 size = g.CalcSize(new GUIContent("Hello World!"));
        GUI.Label(new Rect(Screen.width/2 - size.x/2, Screen.height/4 - size.y/2, Screen.width, Screen.height), "Hello World!", g);

	//Texture2D buttonTexture = new Texture2D(1,1);
	//buttonTexture.SetPixel(0,0,Color.green);
	//buttonTexture.Apply();
	    
	g = new GUIStyle(GUI.skin.button);
	g.fontSize = 30;
	GUIContent retry = new GUIContent("retry");
	size = g.CalcSize(retry);
	
	if(GUI.Button(new Rect(Screen.width/2 - size.x/2, Screen.height/2 - size.y/2, size.x, size.y), retry, g)) {
	    SceneManager.LoadScene("align");
	}
    }
}
