using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(LineRenderer))]
 
public class CircleArcRender : MonoBehaviour
{
    public float xradius;
    public float yradius;
    LineRenderer line;
	
    void Awake()
    {
	line = gameObject.GetComponent<LineRenderer>();
	line.useWorldSpace = false;
    }
	
    public void CreatePoints (float startangle, float lengthangle)
    {
	int segments = (int) (100 * (lengthangle/360));
	
	line.positionCount = (segments + 1);
	float x;
	float y;
	float z = 0f;
		
	float angle = startangle;
		
	for (int i = 0; i < (segments + 1); i++)
	{
	    y = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
	    x = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius;
			
	    line.SetPosition (i, new Vector3(x,y,z));
			
	    angle += (lengthangle / segments);
	}
	//Debug.Log(xradius);
    }
}
