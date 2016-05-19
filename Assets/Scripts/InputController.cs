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

    [SerializeField]
    bool spellTraining = false;

    List<List<GameObject>> lightsGrid = new List<List<GameObject>>();

    List<Vector2>[][] trainingSets;

    List<Vector2> inputSwipe = new List<Vector2>();
    bool swipeStarted = false;

    void Start()
    {
        if (isPlayingLights)
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
        else
        {
            //Load in training sets
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
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        swipeStarted = true;
                        goto case TouchPhase.Moved;
                    case TouchPhase.Moved:
                        inputSwipe.Add(touch.position);
                        break;
                    case TouchPhase.Ended:
                        int index;
                        float score = SpellsRecognizer.Compare(inputSwipe, trainingSets, 64, out index);
                        //Pass the index and score off to whatever to check if it matches/passes
                        swipeStarted = false;
                        break;
                }
            }
            else if(swipeStarted)
            {
                int index;
                float score = SpellsRecognizer.Compare(inputSwipe, trainingSets, 64, out index);
                //Pass the index and score off to whatever to check if it matches/passes
                swipeStarted = false;
            }
        }
	}
}
