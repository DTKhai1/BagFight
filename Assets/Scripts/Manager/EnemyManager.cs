using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager: MonoBehaviour
{
    public int _totalEnemyLeft;
    public int _waveLeft;
    public int _currentEnemyLeft;

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
    public  bool IsEnemyLeft()
    {
        if (_currentEnemyLeft > 0)
        {
            return true;
        }
        return false;
    }
}
