using UnityEngine;
using System.Collections;

public static class PrefStatsScript {

    public static bool isSpells;
    public static int LightsMaxLevel;
    public static int SpellsMaxLevel;

	// Use this for initialization
	static void Start () {
        //SetPlayerPrefs();
	}

    public static void SetPlayerPrefs()
    {
        isSpells = true;
        if (!PlayerPrefs.HasKey("SpellsMaxLevel"))
        {
            PlayerPrefs.SetInt("SpellsMaxLevel", 0);
            PlayerPrefs.SetInt("LightsMaxLevel", 0);
        }
        SpellsMaxLevel = PlayerPrefs.GetInt("SpellsMaxLevel");
        LightsMaxLevel = PlayerPrefs.GetInt("LightsMaxLevel");
    }

    public static void checkMax(int currentLevel)
    {
        Debug.Log("max to check" + currentLevel);
        if (isSpells)
        {
            if (currentLevel > SpellsMaxLevel)
            {
                SpellsMaxLevel = currentLevel;
                PlayerPrefs.SetInt("SpellsMaxLevel", SpellsMaxLevel);
                Debug.Log("New Spells max level: " + SpellsMaxLevel);
                CallWinMessage();
            }
        }
        else //is Lights
        {
            if (currentLevel > LightsMaxLevel)
            {
                LightsMaxLevel = currentLevel;
                PlayerPrefs.SetInt("LightsMaxLevel", LightsMaxLevel);
                Debug.Log("New Lights max level: " + LightsMaxLevel);
                CallWinMessage();
            }
        }
    }

    public static void CallWinMessage()
    {
        GameObject.Find("Main Camera").GetComponent<ProgressionScript>().DisplayWinMessage(isSpells);
    }
}
