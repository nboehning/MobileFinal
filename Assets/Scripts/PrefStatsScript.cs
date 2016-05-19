using UnityEngine;
using System.Collections;

public static class PrefStatsScript {
    public enum GameType { INFINITE, TIME, LEVELS };

    public static bool isSpells;
    public static GameType gameType;

    public static int LightsMaxLevel;
    public static int LightsTimeLevels;
    public static int LightsInfiniteLevels;

    public static int SpellsMaxLevel;
    public static int SpellsTimeLevels;
    public static int SpellsInfiniteLevels;

    // Use this for initialization
    static void Start () {
        //SetPlayerPrefs();
	}

    public static void SetPlayerPrefs()
    {
        //isSpells = true;
        if (!PlayerPrefs.HasKey("SpellsMaxLevel"))
        {
            PlayerPrefs.SetInt("SpellsMaxLevel", 0);
            PlayerPrefs.SetInt("SpellsTimeLevels", 0);
            PlayerPrefs.SetInt("SpellsInfiniteLevels", 0);
            PlayerPrefs.SetInt("LightsMaxLevel", 0);
            PlayerPrefs.SetInt("LightsTimeLevels", 0);
            PlayerPrefs.SetInt("LightsInfiniteLevels", 0);
        }
        SpellsMaxLevel = PlayerPrefs.GetInt("SpellsMaxLevel");
        SpellsTimeLevels = PlayerPrefs.GetInt("SpellsTimeLevels");
        SpellsInfiniteLevels = PlayerPrefs.GetInt("SpellsInfiniteLevels");
        LightsMaxLevel = PlayerPrefs.GetInt("LightsMaxLevel");
        LightsTimeLevels = PlayerPrefs.GetInt("LightsTimeLevels");
        LightsInfiniteLevels = PlayerPrefs.GetInt("LightsInfiniteLevels");
    }

    public static void checkMax(int currentLevel)
    {
        Debug.Log("max to check" + currentLevel);
        if (isSpells)
        {
            switch(gameType)
            {
                case GameType.INFINITE:
                    if(currentLevel > SpellsInfiniteLevels)
                    {
                        SpellsInfiniteLevels = currentLevel;
                        PlayerPrefs.SetInt("SpellsInfiniteLevels", SpellsInfiniteLevels);
                        Debug.Log("New Spells Infinite max level: " + SpellsInfiniteLevels);
                        CallWinMessage();
                    }
                    break;
                case GameType.LEVELS:
                    if (currentLevel > SpellsMaxLevel)
                    {
                        SpellsMaxLevel = currentLevel;
                        PlayerPrefs.SetInt("SpellsMaxLevel", SpellsMaxLevel);
                        Debug.Log("New Spells Levels max level: " + SpellsMaxLevel);
                        CallWinMessage();
                    }
                    break;
                case GameType.TIME:
                    if (currentLevel > SpellsTimeLevels)
                    {
                        SpellsTimeLevels = currentLevel;
                        PlayerPrefs.SetInt("SpellsTimeLevels", SpellsTimeLevels);
                        Debug.Log("New Spells Timed max level: " + SpellsTimeLevels);
                        CallWinMessage();
                    }
                    break;
                default:
                    Debug.LogError("Game Type Not Set before game start.");
                    break;
            }

            
        }
        else //is Lights
        {
            switch (gameType)
            {
                case GameType.INFINITE:
                    if (currentLevel > LightsInfiniteLevels)
                    {
                        LightsInfiniteLevels = currentLevel;
                        PlayerPrefs.SetInt("LightsInfiniteLevels", LightsInfiniteLevels);
                        Debug.Log("New Lights Infinite max level: " + LightsInfiniteLevels);
                        CallWinMessage();
                    }
                    break;
                case GameType.LEVELS:
                    if (currentLevel > LightsMaxLevel)
                    {
                        LightsMaxLevel = currentLevel;
                        PlayerPrefs.SetInt("LightsMaxLevel", LightsMaxLevel);
                        Debug.Log("New Lights max level: " + LightsMaxLevel);
                        CallWinMessage();
                    }
                    break;
                case GameType.TIME:
                    if (currentLevel > LightsTimeLevels)
                    {
                        LightsTimeLevels = currentLevel;
                        PlayerPrefs.SetInt("SpellsTimeLevels", LightsTimeLevels);
                        Debug.Log("New Lights Timed max level: " + LightsTimeLevels);
                        CallWinMessage();
                    }
                    break;
                default:
                    Debug.LogError("Game Type Not Set before game start.");
                    break;
            }

            
        }
    }

    public static void CallWinMessage()
    {
        GameObject.Find("Main Camera").GetComponent<ProgressionScript>().DisplayWinMessage(isSpells);
    }
}
