using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public enum MenuState
{
    MAIN,
    CREDITS,
    SETTINGS,
    DIRECTIONS,
    TRANSITIONING
}
public class SwipeMenu : MonoBehaviour {

    public float swipeMinLength;

    MenuState curMenu = MenuState.MAIN;
    MenuState lastMenu;
    MenuState nextMenu;
    Vector2 newMenuPos;

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject directionsMenu;

	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MenuTransitionRight();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            MenuTransitionLeft();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MenuTransitionDown();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MenuTransitionUp();
        }

	    foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Moved && touch.deltaPosition.magnitude >= swipeMinLength)
            {
                if (touch.deltaPosition.x > touch.deltaPosition.y)
                {
                    //Swipe direction is Down or Right
                    if (touch.deltaPosition.x > -touch.deltaPosition.y)
                    {
                        //Slide menu right
                        MenuTransitionRight();
                    }
                    else
                    {
                        //Slide menu down
                        MenuTransitionDown();
                    }
                }
                else
                {
                    //Swipe direction is Up or Left
                    if (touch.deltaPosition.x > -touch.deltaPosition.y)
                    {
                        //Slide menu up
                        MenuTransitionUp();
                    }
                    else
                    {
                        //Slide menu left
                        MenuTransitionLeft();
                    }
                }
            }
        }
	}

    void MenuTransitionLeft()
    {
        newMenuPos = new Vector2(Screen.width * 1.5f, Screen.height / 2f);
        switch (curMenu)
        {
            case MenuState.MAIN:
                //Credits
                creditsMenu.SetActive(true);
                creditsMenu.transform.position = newMenuPos;
                curMenu = MenuState.TRANSITIONING;
                lastMenu = MenuState.MAIN;
                nextMenu = MenuState.CREDITS;
                StartCoroutine(ShiftMenu());
                break;
            case MenuState.SETTINGS:
                //Main
                mainMenu.SetActive(true);
                mainMenu.transform.position = newMenuPos;
                curMenu = MenuState.TRANSITIONING;
                lastMenu = MenuState.SETTINGS;
                nextMenu = MenuState.MAIN;
                StartCoroutine(ShiftMenu());
                break;
            case MenuState.CREDITS:
                //Directions
                directionsMenu.SetActive(true);
                directionsMenu.transform.position = newMenuPos;
                curMenu = MenuState.TRANSITIONING;
                lastMenu = MenuState.CREDITS;
                nextMenu = MenuState.DIRECTIONS;
                StartCoroutine(ShiftMenu());
                break;
        }
    }

    

    void MenuTransitionRight()
    {
        newMenuPos = new Vector2(-(Screen.width / 2f), Screen.height / 2f);
        switch (curMenu)
        {
            case MenuState.MAIN:
                //Settings
                settingsMenu.SetActive(true);
                settingsMenu.transform.position = newMenuPos;
                curMenu = MenuState.TRANSITIONING;
                lastMenu = MenuState.MAIN;
                nextMenu = MenuState.SETTINGS;
                StartCoroutine(ShiftMenu());
                break;
            case MenuState.DIRECTIONS:
                //Credits
                creditsMenu.SetActive(true);
                creditsMenu.transform.position = newMenuPos;
                curMenu = MenuState.TRANSITIONING;
                lastMenu = MenuState.DIRECTIONS;
                nextMenu = MenuState.CREDITS;
                StartCoroutine(ShiftMenu());
                break;
            case MenuState.CREDITS:
                //Main
                mainMenu.SetActive(true);
                mainMenu.transform.position = newMenuPos;
                curMenu = MenuState.TRANSITIONING;
                lastMenu = MenuState.CREDITS;
                nextMenu = MenuState.MAIN;
                StartCoroutine(ShiftMenu());
                break;
        }
    }

    void MenuTransitionDown()
    {
        newMenuPos = new Vector2(Screen.width / 2f, Screen.height * 1.5f);
        switch (curMenu)
        {
            case MenuState.MAIN:
                //Directions
                directionsMenu.SetActive(true);
                directionsMenu.transform.position = newMenuPos;
                curMenu = MenuState.TRANSITIONING;
                lastMenu = MenuState.MAIN;
                nextMenu = MenuState.DIRECTIONS;
                StartCoroutine(ShiftMenu());
                break;
            case MenuState.SETTINGS:
                //Credits
                creditsMenu.SetActive(true);
                creditsMenu.transform.position = newMenuPos;
                curMenu = MenuState.TRANSITIONING;
                lastMenu = MenuState.SETTINGS;
                nextMenu = MenuState.CREDITS;
                StartCoroutine(ShiftMenu());
                break;
            case MenuState.DIRECTIONS:
                //Settings
                settingsMenu.SetActive(true);
                settingsMenu.transform.position = newMenuPos;
                curMenu = MenuState.TRANSITIONING;
                lastMenu = MenuState.DIRECTIONS;
                nextMenu = MenuState.SETTINGS;
                StartCoroutine(ShiftMenu());
                break;
        }
    }

    void MenuTransitionUp()
    {
        newMenuPos = new Vector2(Screen.width / 2f, -(Screen.height / 2f));
        switch (curMenu)
        {
            case MenuState.DIRECTIONS:
                //Main
                mainMenu.SetActive(true);
                mainMenu.transform.position = newMenuPos;
                curMenu = MenuState.TRANSITIONING;
                lastMenu = MenuState.DIRECTIONS;
                nextMenu = MenuState.MAIN;
                StartCoroutine(ShiftMenu());
                break;
            case MenuState.SETTINGS:
                //Directions
                directionsMenu.SetActive(true);
                directionsMenu.transform.position = newMenuPos;
                curMenu = MenuState.TRANSITIONING;
                lastMenu = MenuState.SETTINGS;
                nextMenu = MenuState.DIRECTIONS;
                StartCoroutine(ShiftMenu());
                break;
            case MenuState.CREDITS:
                //Settings
                settingsMenu.SetActive(true);
                settingsMenu.transform.position = newMenuPos;
                curMenu = MenuState.TRANSITIONING;
                lastMenu = MenuState.CREDITS;
                nextMenu = MenuState.SETTINGS;
                StartCoroutine(ShiftMenu());
                break;
        }
    }

    IEnumerator ShiftMenu()
    {
        GameObject incomingMenu = mainMenu;
        GameObject outgoingMenu = mainMenu;
        switch (nextMenu)
        {
            case MenuState.CREDITS:
                incomingMenu = creditsMenu;
                break;
            case MenuState.SETTINGS:
                incomingMenu = settingsMenu;
                break;
            case MenuState.DIRECTIONS:
                incomingMenu = directionsMenu;
                break;
        }
        switch (lastMenu)
        {
            case MenuState.CREDITS:
                outgoingMenu = creditsMenu;
                break;
            case MenuState.SETTINGS:
                outgoingMenu = settingsMenu;
                break;
            case MenuState.DIRECTIONS:
                outgoingMenu = directionsMenu;
                break;
        }
        Vector2 screenCenter = outgoingMenu.transform.position;
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            Vector2 move = incomingMenu.transform.position;
            incomingMenu.transform.position = Vector2.Lerp(newMenuPos, screenCenter, time);
            move -= (Vector2)incomingMenu.transform.position;
            outgoingMenu.transform.Translate((-1f * move));
            yield return null;
        }
        outgoingMenu.SetActive(false);
        curMenu = nextMenu;
    }

    public void _LoadLevel()
    {
        SceneManager.LoadScene("LoadMap");
    }



}
