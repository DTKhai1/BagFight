using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public WeaponData _weaponData;
    public Image _image;
    public Button _statButton;
    public Transform _parentAfterDrag;
    public int _inventoryPosition;

    private Canvas _mainCanvas;
    InventoryManager _inventoryManager;
    private void Awake()
    {
        _mainCanvas = GetComponentInParent<Canvas>();
        _inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        _parentAfterDrag = transform.parent;

        transform.SetParent(_mainCanvas.transform);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_parentAfterDrag);
        _image.raycastTarget = true;
    }
    public void InitializeItem(WeaponData weaponData)
    {
        _weaponData = weaponData;
        _image.sprite = _weaponData._icon;
        _image.color = _weaponData._backgroundColor;
        _statButton.onClick.AddListener(() => _inventoryManager.OpenStatPanel(weaponData, _inventoryPosition));
    }
}