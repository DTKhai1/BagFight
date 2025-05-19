using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "ScriptableObjects/BP")]
public class ProgressTrackManager : ScriptableObject
{
    public int _currentTier = 0;
    public int _currentExp;
}
