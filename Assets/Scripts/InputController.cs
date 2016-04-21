using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// @author Marshall R. Mason
/// This script handles the inputs and player controls for Lights and Spells gameplay modes.
/// </summary>
public class InputController : MonoBehaviour {

    public int lightsRows = 7;
    public int lightsColumns = 6;

    public float lightGridSize = .2f;
    public float lightGridOffset = 0f;

    public GameObject lightPrefab;
    public bool isPlayingLights = false;

    List<List<GameObject>> lightsGrid = new List<List<GameObject>>();
    
    void Start()
    {
        Vector3 nextLightPos;
        for (int i = 0; i < lightsRows; i++)
        {
            List<GameObject> tempList = new List<GameObject>();
            for (int j = 0; j < lightsColumns; j++)
            {
                nextLightPos = new Vector3(((lightGridSize * i) + lightGridOffset), ((lightGridSize * j) + lightGridOffset), 0);
                GameObject newLight = (GameObject)Instantiate(lightPrefab, nextLightPos, Quaternion.Euler(0, 0, 0));
                tempList.Add(newLight);
            }
            lightsGrid.Add(tempList);
        }
    }

    void Update ()
    {
	    if (isPlayingLights)
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    Collider2D hitCollider = Physics2D.OverlapPoint(touchPos);
                    if (hitCollider.transform.tag == "Light")
                    {
                        hitCollider.GetComponent<LightsToggle>().enabled = true;
                    }
                }
            }
        }
	}
}
