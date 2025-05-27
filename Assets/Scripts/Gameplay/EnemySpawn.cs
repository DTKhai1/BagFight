using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour, IFixedUpdateObserver
{
    public GameObject _enemyPrefab;
    public Collider2D _spawnArea;
    private int _totalWave = 2;
    private int _totalEnemy = 10;
    private int _enemyCount;
    private int _groupEnemyAmount;
    private int GroupEnemyAmount
    {
        get { return _groupEnemyAmount; }
        set
        {
            _groupEnemyAmount = value;
            if (_groupEnemyAmount <= 0)
            {
                _isSpawning = false;
            }
            else
            {
                _isSpawning = true;
            }
        }
    }

    private float _groupSpawnInterval = 2.5f;
    private float _groupSpawnTimer;
    private float _spawnTime = 0.25f;
    private float _spawnInterval = 0.5f;
    private bool _isSpawning;

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
        _groupSpawnTimer = 2f;
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
        _spawnTime += Time.fixedDeltaTime;
        _groupSpawnTimer += Time.fixedDeltaTime;
        if (_gameManager.CurrentPlayingState == PlayingState.EnemySpawn)
        {
            if(_groupSpawnTimer >= _groupSpawnInterval)
            {
                GroupEnemyAmount = 3;
                _groupSpawnTimer = 0f;
            }
            if(_spawnTime >= _spawnInterval && _isSpawning)
            {
                SpawnEnemy();
                _spawnTime = 0;
                GroupEnemyAmount--;
                Debug.Log("groupEnemyAmount: " + GroupEnemyAmount + " spawning? " + _isSpawning);
            }
        }
    }
}
