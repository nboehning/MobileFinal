using UnityEngine;
using System.Collections;

public class aa : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    Debug.Log("Width: " + GetComponent<RectTransform>().rect.width);
	    Debug.Log("Height: " + GetComponent<RectTransform>().rect.height);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
