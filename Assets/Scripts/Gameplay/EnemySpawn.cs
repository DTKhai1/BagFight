using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour, IFixedUpdateObserver
{
    public GameObject _enemyPrefab;
    public Collider2D _spawnArea;
    public float _spawnInterval = 2f;
    public int _totalWave = 3;
    public int _totalEnemy = 15;
    private int _enemyCount;
    private float _spawnTimer = 0f;
    private GameManager _gameManager;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _spawnArea = GetComponent<Collider2D>();
    }
    private void Start()
    {
        ResetForNewWave();
        _gameManager._enemyManager._waveLeft = _totalWave;
    }
    private void SpawnEnemy()
    {
        if (_enemyCount < _totalEnemy)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(_spawnArea.bounds.min.x, _spawnArea.bounds.max.x), Random.Range(_spawnArea.bounds.min.y, _spawnArea.bounds.max.y));
            GameObject enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            _enemyCount++;
            _gameManager._enemyManager._currentEnemyLeft++;
        }
    }
    public void ResetForNewWave()
    {
        _enemyCount = 0;
        _gameManager._enemyManager._currentEnemyLeft = 0;
        _gameManager._enemyManager._totalEnemyLeft = _totalEnemy;
    }
    public void ContinueLevel()
    {
        ResetForNewWave();
        _gameManager.ChangeToEnemySpawn();
    }
    private void OnEnable()
    {
        UpdateManager.RegisterFixedUpdateObserver(this);
    }
    private void OnDestroy()
    {
        UpdateManager.UnregisterFixedUpdateObserver(this);
    }
    public void ObservedFixedUpdate()
    {
        _spawnTimer += Time.fixedDeltaTime;
        if(_gameManager.CurrentPlayingState == PlayingState.EnemySpawn)
        {
            if(_spawnTimer >= _spawnInterval)
            {
                SpawnEnemy();
                _spawnTimer = 0f;
            }
        }
    }
}
