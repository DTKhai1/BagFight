using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour, IUpdateObserver
{
    GameManager _gameManager;

    public WeaponDictionary _weaponDictionary;
    public GameObject _weaponShopSlotPrefab;
    public RectTransform _content;
    public TMP_Text _resetText;
    public GameObject _statPanel;

    private TaskCompletionSource<bool> _tcs;
    public RectTransform _shopUITransform;
    private Vector2 _targetPosition;
    private bool _isMoving;

    private int _resetAttemptsLeft;
    private int _numberOfWeapons;
    private int _numberOfSlots = 3;
    private InventoryManager _inventoryManager;
    private List<GameObject> _inventoryList = new List<GameObject>();

    public bool _isChangeStateFinish;
    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _weaponDictionary = _gameManager._weaponDictionary;
        _inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
        _shopUITransform = GameObject.FindGameObjectWithTag("ShopUI").GetComponent<RectTransform>();
    }
    private void Start()
    {
        _resetAttemptsLeft = 2;
        _resetText.text = "Reset: " + _resetAttemptsLeft.ToString();
        _numberOfWeapons = _weaponDictionary._weaponList.Count;
        _gameManager._weaponShop = this;
        ShowShopUI();
        _isMoving = false;
    }
    public void ShowShopUI()
    {
        for (int i = 0; i < _numberOfSlots; i++)
        {
            GameObject weaponShopSlot = Instantiate(_weaponShopSlotPrefab, _content);
            _inventoryList.Add(weaponShopSlot);
            WeaponShopSlot slotScript = weaponShopSlot.GetComponent<WeaponShopSlot>();
            int _index = Random.Range(0, _numberOfWeapons);
            int order = i;
            slotScript._weaponIcon.sprite = _weaponDictionary._weaponList[_index]._icon;
            slotScript._weaponPrice.text = "-" + _weaponDictionary._weaponList[_index]._price.ToString() + "G";
            slotScript._buyButton.onClick.AddListener(() => BuyWeapon(_index, order));
            slotScript._statButton.onClick.AddListener(() => OpenStatPanel(_weaponDictionary._weaponList[_index]));
        }
    }
    public void ResetShop()
    {
        if(_resetAttemptsLeft <= 0)
        {
            Debug.Log("No more attempts left to reset the shop.");
            return;
        }
        foreach (GameObject slot in _inventoryList)
        {
            Destroy(slot);
        }
        _inventoryList.Clear();
        ShowShopUI();
        _resetAttemptsLeft--;
        _resetText.text = "Reset: " + _resetAttemptsLeft.ToString();
    }
    public void BuyWeapon(int index, int order)
    {
        if (_inventoryManager.IsInventoryFull())
        {
            Debug.Log("Inventory is full.");
            return;
        }
        WeaponData _copyOfWeapon = ScriptableObject.CreateInstance<WeaponData>();
        _copyOfWeapon._weaponName = _weaponDictionary._weaponList[index]._weaponName;
        _copyOfWeapon._basedamage = _weaponDictionary._weaponList[index]._basedamage;
        _copyOfWeapon._baseAttackSpeed = _weaponDictionary._weaponList[index]._baseAttackSpeed;
        _copyOfWeapon._icon = _weaponDictionary._weaponList[index]._icon;
        _copyOfWeapon._price = _weaponDictionary._weaponList[index]._price;
        _copyOfWeapon._level = _weaponDictionary._weaponList[index]._level;
        _copyOfWeapon._wpRarity = _weaponDictionary._weaponList[index]._wpRarity;
        _copyOfWeapon._type = _weaponDictionary._weaponList[index]._type;
        _copyOfWeapon._target = _weaponDictionary._weaponList[index]._target;
        _inventoryManager.AddItem(_copyOfWeapon);
        _inventoryList[order].SetActive(false);
    }
    public async Task MoveShopUI()
    {
        _isMoving = true;
        _tcs = new TaskCompletionSource<bool>();

        if (_gameManager.CurrentPlayingState == PlayingState.EnemySpawn)
        {
            _targetPosition = new Vector2(
                _shopUITransform.anchoredPosition.x,
                _shopUITransform.anchoredPosition.y - 1000
            );
        }
        else
        {
            _targetPosition = new Vector2(
                _shopUITransform.anchoredPosition.x,
                _shopUITransform.anchoredPosition.y + 1000
            );
            _resetAttemptsLeft = 3;
            ResetShop();
        }

        UpdateManager.RegisterUpdateObserver(this);
        await _tcs.Task;
        UpdateManager.UnregisterUpdateObserver(this);
    }

    public void ObservedUpdate()
    {
        if (!_isMoving || _tcs == null) return;

        _shopUITransform.anchoredPosition = Vector2.MoveTowards(
            _shopUITransform.anchoredPosition,
            _targetPosition,
            1000 * Time.deltaTime
        );

        if (Vector2.Distance(_shopUITransform.anchoredPosition, _targetPosition) < 0.01f)
        {
            _isMoving = false;
            _tcs.TrySetResult(true);
        }
    }
    public void OpenStatPanel(WeaponData weaponData)
    {
        _statPanel.SetActive(true);
        _statPanel.GetComponent<StatPanel>().Initialize(weaponData, -1);
    }
}
