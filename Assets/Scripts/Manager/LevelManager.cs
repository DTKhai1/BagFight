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
    public int _totalEnemyLeft;
    public int _waveLeft;
    public int _currentEnemyLeft;

    public int _maxLevel;
    public int _currentLevel;
    public LevelType _currentLevelType
    {
        get
        {
            switch (_currentLevel % 5)
            {
                case 0:
                    return LevelType.Boss;
                default:
                    return LevelType.Normal;
            }
        }
    }
    public int _currentWave;
    public bool IsLevelFinished()
    {
        if (_waveLeft <= 0)
        {
            return true;
        }
        return false;
    }
    public bool IsWaveClear()
    {
        if (_totalEnemyLeft <= 0)
        {
            return true;
        }
        return false;
    }
    public bool IsAnyEnemyLeft()
    {
        if (_currentEnemyLeft > 0)
        {
            return true;
        }
        return false;
    }
}
