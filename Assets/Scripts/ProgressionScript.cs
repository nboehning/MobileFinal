using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressionScript : MonoBehaviour {

    public float lightsBeginningTime = 40f;
    public float spellsBeginningTime = 20f;
    public float percentageReduced1 = 0.059f;
    public int levelThreshold2 = 10;
    public float percentageReduced2 = 0.035f;
    public int levelThreshold3 = 25;
    public float percentageReduced3 = 0.028f;
    public float percentForWrongPrompt = 0.318f;
    public float modTime;
    float currentTime;

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
