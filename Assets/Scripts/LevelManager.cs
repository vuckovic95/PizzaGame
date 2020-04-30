using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using NaughtyAttributes;

public class LevelManager : MonoBehaviour
{
    [BoxGroup("Level Number")] public int levelNum;
    [BoxGroup("Level Prefabs")] public List<GameObject> levelList;
    
    private Transform tr;

    private void Awake()
    {
        Init();
    }
    
    public void Init()
    {
        GlobalManager.LevelManager = this;
        tr = transform;
        PopulateLevelList();
    }

    public void SetLevel(int _levelNum)   // for prefab levels
    {
        if (levelList.Count == 0)
            return;

        foreach(GameObject g in levelList) 
        {
            g.SetActive(false);
        }

        levelNum = _levelNum % levelList.Count;
        if(levelNum == 0)
        {
            levelNum = levelList.Count;
        }

        levelList[levelNum - 1].SetActive(true);
    }

    public void ResetLevel()
    {
        //TODO
    }

    public int NewRandomInt(List<int> _notUsed, int fromInclusive, int toInclusive)
    {
        int value = Random.Range(fromInclusive, toInclusive + 1);

        foreach(int i in _notUsed)
        {
            if(value == i)
            {
                return NewRandomInt(_notUsed, fromInclusive, toInclusive);
            }
        }

        return value;
    }

    public void PopulateLevelList()
    {
        foreach(Transform t in tr)
        {
            levelList.Add(t.gameObject);
        }
    }
}
