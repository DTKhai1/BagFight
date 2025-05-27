using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType
{
    Normal,
    Boss
}
public class LevelManager : MonoBehaviour
{
    public int _maxLevel;
    public int _currentLevel;
    public LevelType _currentLevelType
    {
        get
        {
            if (_currentLevel % 5 == 0)
            {
                return LevelType.Boss;
            }
            else
            {
                return LevelType.Normal;
            }
        }
    }
}
