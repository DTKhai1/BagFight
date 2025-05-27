using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public int _progressTrackXP;
    public int _gold;

    [NonSerialized]public int _maxLevelProgress = 30;
    public List<bool> _isRewardReceived;
}
