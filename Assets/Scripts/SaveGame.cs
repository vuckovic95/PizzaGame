using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class SaveGame : MonoBehaviour
{
    void Awake()
    {
        GlobalManager.SaveGame = this;
    }
    
    public void SaveLevelNum(int value)
    {
        PlayerPrefs.SetInt("lvlNum", value);
    }

    public void SaveInGameLevel(int value)
    {
        PlayerPrefs.SetInt("inGameLvl", value);
    }

    public int LoadLevelNum()
    {
        int value = PlayerPrefs.GetInt("lvlNum");
        return value;
    }

    public int LoadInGameLevel()
    {
        int value = PlayerPrefs.GetInt("inGameLvl");

        if(value == 0)   // firstLoad
        {
            value = 1;
            PlayerPrefs.SetInt("inGameLvl", value);
        }

        return value;
    }
}
