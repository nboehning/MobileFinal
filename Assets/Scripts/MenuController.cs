using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Variables for menu transitions
	public GameObject[] menuItems;
    private int curMenu = 0;
    private int prevMenu;
	private bool isSpells;

    // Variables for swiping
    Vector2 prevPosition;
    Vector2 curPosition;
    float touchDelta;
    int iComfort = 75;

    // Update is called once per frame
    void Update ()
	{
	    switch (curMenu)
	    {
            case 0:
	            CheckSwipeMain();
	            break;
            case 1:
	            CheckSwipeGame();
	            break;
            case 2:
	            CheckSwipeGame();
	            break;
            case 3:
	            CheckSwipeOptions();
	            break;
	    }
	}

    void CheckSwipeMain()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                prevPosition = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                curPosition = Input.GetTouch(0).position;

                touchDelta = curPosition.magnitude - prevPosition.magnitude;

                if (Mathf.Abs(touchDelta) > iComfort)
                {
                    if (touchDelta > 0)
                    {
                        if (Mathf.Abs(curPosition.x - prevPosition.x) < Mathf.Abs(curPosition.y - prevPosition.y))
                        {
                            // Up
                            prevMenu = curMenu;
                            curMenu = 3;
                            menuItems[prevMenu].SetActive(false);
                            menuItems[curMenu].SetActive(true);
                        }
                    }
                }
            }

        }
    }

    void CheckSwipeGame()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                prevPosition = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                curPosition = Input.GetTouch(0).position;

                touchDelta = curPosition.magnitude - prevPosition.magnitude;

                if (Mathf.Abs(touchDelta) > iComfort)
                {
                    if (touchDelta > 0)
                    {
                        if (Mathf.Abs(curPosition.x - prevPosition.x) > Mathf.Abs(curPosition.y - prevPosition.y))
                        {
                            // Right
                            prevMenu = curMenu;
                            curMenu = 0;
                            menuItems[curMenu].SetActive(true);
                            menuItems[prevMenu].SetActive(false);
                        }
                        else
                        {
                            // Up
                            prevMenu = curMenu;
                            curMenu = 3;
                            menuItems[prevMenu].SetActive(false);
                            menuItems[curMenu].SetActive(true);
                        }
                    }
                }
            }
        }
    }

    void CheckSwipeOptions()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                prevPosition = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                curPosition = Input.GetTouch(0).position;

                touchDelta = curPosition.magnitude - prevPosition.magnitude;

                if (Mathf.Abs(touchDelta) > iComfort)
                {
                    if (touchDelta > 0)
                    {
                        if (Mathf.Abs(curPosition.x - prevPosition.x) < Mathf.Abs(curPosition.y - prevPosition.y))
                        {
                            // swipe Up to switch to options from main menu
                            curMenu = prevMenu;
                            prevMenu = 3;
                            menuItems[prevMenu].SetActive(false);
                            menuItems[curMenu].SetActive(true);
                        }
                    }
                }
            }

        }
    }

	public void BtnPlayLights()
	{
		isSpells = false;
	    menuItems[curMenu].SetActive(false);
	    curMenu = 1;
        menuItems[curMenu].SetActive(true);
    }

	public void BtnPlaySpells()
	{
		isSpells = true;
        menuItems[curMenu].SetActive(false);
        curMenu = 2;
        menuItems[curMenu].SetActive(true);
    }

    public void BtnPlayLevels()
    {
        if (isSpells)
        {
            // Code to load Spells Levels
            Debug.Log("Plays Spells Levels");
            SceneManager.LoadScene("LevelScene");
        }
        else
        {
            // Code to Load Lights Levels
            Debug.Log("Plays Lights Levels");
            SceneManager.LoadScene("LevelScene");
        }
        
    }

    public void BtnPlayEndless()
    {
        if (isSpells)
        {
            // Code to load Spells Endless
            Debug.Log("Plays Spells Endless");
            SceneManager.LoadScene("LevelScene");
        }
        else
        {
            // Code to Load Lights Endless
            Debug.Log("Plays Lights Endless");
            SceneManager.LoadScene("LevelScene");
        }
    }

    public void BtnPlayTime()
    {
        if (isSpells)
        {
            // Code to load Spells Time
            Debug.Log("Plays Spells Time");
            SceneManager.LoadScene("LevelScene");
        }
        else
        {
            // Code to Load Lights Time
            Debug.Log("Plays Lights Time");
            SceneManager.LoadScene("LevelScene");
        }
    }
}
