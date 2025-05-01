using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenManager : MonoBehaviour
{
    private LevelManager _levelManager;
    private void Awake()
    {
        _levelManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelManager>();
    }
    public void GoToPlay()
    {
        _levelManager.ChangeScene("Playing");
    }
}
