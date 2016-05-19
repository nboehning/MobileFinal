using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressionScript : MonoBehaviour {

    //elements designed for the game
    [Tooltip("Beginning time for the in lights game per level in Infinite mode.")]
    public float lightsBeginningTime = 40f;
    [Tooltip("Beginning time for the in spells game per level in Infinite mode.")]
    public float spellsBeginningTime = 20f;
    [Tooltip("Percentage of time removed from current counter for next level. 1st tier.")]
    public float percentageReduced1 = 0.059f;
    [Tooltip("The level at which the percentage subtracted from the time counter is changed from percentageReduced1 to percentageReduced2")]
    public int levelThreshold2 = 10;
    [Tooltip("Percentage of time removed from current counter for next level. 2nd tier.")]
    public float percentageReduced2 = 0.035f;
    [Tooltip("The level at which the percentage subtracted from the time counter is changed from percentageReduced2 to percentageReduced3")]
    public int levelThreshold3 = 25;
    [Tooltip("Percentage of time removed from current counter for next level. 3rd tier.")]
    public float percentageReduced3 = 0.028f;
    [Tooltip("percentage of time in game from level/infinite mode that the player is notified they are reaching the end.")]
    public float percentForWrongPrompt = 0.318f;
    //placeholder. Keeps track of start time for current level (infinite) or total time (timed)
    float modTime;
    //current time left in level/game
    float currentTime;
    //keeps track of the levels completed. Will not count current level passed. 
    int currentLevel;

    bool checkedMax;

    public Text[] timeLeftTitles;
    public Text timeLeftText;
    Color outlineColor;
    public Text levelText;
    public Text congratText;
    public string lightsWinMessage;
    public string spellsWinMessage;
    public GameObject resetButton;
    public Button confirmButton;

    int sceneNumber;

	// Use this for initialization
	void Start () {
        PrefStatsScript.SetPlayerPrefs();
        if(PrefStatsScript.isSpells)
            currentTime = modTime = spellsBeginningTime;
        else
            currentTime = modTime = lightsBeginningTime;
        currentLevel = 0;
        checkedMax = false;
        outlineColor = timeLeftText.GetComponent<Outline>().effectColor;

        SetTheMarquee();
        resetButton.SetActive(false);
        confirmButton.interactable = true;
        congratText.gameObject.SetActive(false);
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }
	
	// Update is called once per frame
	void Update () {
        TickTick();
	}

    void Warning()
    {
        timeLeftText.GetComponent<Outline>().effectColor = Color.red;
        foreach(Text item in timeLeftTitles)
        {
            item.GetComponent<Outline>().effectColor = Color.red;
        }
    }

    public void _CallForSuccess()
    {
        SuccessRun();
    }

    public void _CallForReset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNumber);
    }

    void SuccessRun()
    {

        currentLevel += 1;
        if (PrefStatsScript.gameType == PrefStatsScript.GameType.INFINITE)
        {
            float mod;
            if (currentLevel <= levelThreshold2)
                mod = percentageReduced1;
            else if (currentLevel <= levelThreshold3)
                mod = percentageReduced2;
            else
                mod = percentageReduced3;
            modTime -= modTime * mod;
            currentTime = modTime;
            SetTheMarquee();
        }
    }

    void SetTheMarquee()
    {
        timeLeftText.text = currentTime.ToString("##.000");
        levelText.text = (currentLevel + 1).ToString();
        timeLeftText.GetComponent<Outline>().effectColor = outlineColor;
        foreach (Text item in timeLeftTitles)
        {
            item.GetComponent<Outline>().effectColor = outlineColor;
        }
    }

    void TickTick()
    {
        if (currentTime > 0)
        {
            
            timeLeftText.text = currentTime.ToString("##.000");
            currentTime -= Time.deltaTime;
            if (currentTime < modTime * percentForWrongPrompt)
            {
                Warning();
            }
        }
        else
        {
            if (!checkedMax)
            {
                currentTime = 0f;
                timeLeftText.text = currentTime.ToString("##.000");
                FailRun();
            }
        }
    }

    void FailRun()
    {
        checkedMax = true;
        Time.timeScale = 0;
        PrefStatsScript.checkMax(currentLevel);
        resetButton.SetActive(true);
        confirmButton.interactable = false;
    }

    public void DisplayWinMessage(bool isSpells)
    {
        congratText.gameObject.SetActive(true);
        if (isSpells)
        {
            congratText.text = spellsWinMessage;
        }
        else //is Lights
        {
            congratText.text = lightsWinMessage;
        }
    }
}
