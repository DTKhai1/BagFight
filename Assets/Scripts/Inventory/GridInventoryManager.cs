using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GridInventoryManager : MonoBehaviour
{
    [SerializeField] private int gridWidth = 3;
    [SerializeField] private int gridHeight = 3;
    [SerializeField] private Transform gridContainer;
    [SerializeField] private Color validPlacementColor = Color.green;
    [SerializeField] private Color invalidPlacementColor = Color.red;
    [SerializeField] private Color normalColor = Color.white;

    private InventorySlotUI[,] gridSlots;
    private List<Vector2Int> highlightedCells = new List<Vector2Int>();

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        gridSlots = new InventorySlotUI[gridWidth, gridHeight];

        // Get all slot UIs in order
        InventorySlotUI[] slots = gridContainer.GetComponentsInChildren<InventorySlotUI>();

        int index = 0;
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                gridSlots[x, y] = slots[index];
                gridSlots[x, y].Initialize(new Vector2Int(x, y));
                index++;
            }
        }
    }

    public void ShowPlacementPreview(WeaponData weapon, Vector2Int position)
    {
        // Clear previous highlights
        ClearHighlights();

        // Get affected cells
        List<Vector2Int> affectedCells = GetAffectedCells(weapon, position);
        bool isValidPlacement = CanPlaceWeapon(weapon, position);

        // Highlight affected cells
        foreach (Vector2Int cell in affectedCells)
        {
            if (IsValidGridPosition(cell))
            {
                highlightedCells.Add(cell);
                gridSlots[cell.x, cell.y].Highlight(isValidPlacement ? validPlacementColor : invalidPlacementColor);
            }
        }
    }

    private List<Vector2Int> GetAffectedCells(WeaponData weapon, Vector2Int position)
    {
        List<Vector2Int> cells = new List<Vector2Int>();

        switch (weapon.shape)
        {
            case WeaponShape.Single:
                cells.Add(position);
                break;

            case WeaponShape.Straight:
                if (weapon.isVertical)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        cells.Add(new Vector2Int(position.x, position.y + i));
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        cells.Add(new Vector2Int(position.x + i, position.y));
                    }
                }
                break;

            case WeaponShape.LShaped:
                cells.Add(position); // Base position
                cells.Add(new Vector2Int(position.x + 1, position.y)); // Horizontal part
                cells.Add(new Vector2Int(position.x, position.y + 1)); // Vertical part
                break;
        }

        return cells;
    }

    public void ClearHighlights()
    {
        foreach (Vector2Int cell in highlightedCells)
        {
            if (IsValidGridPosition(cell))
            {
                gridSlots[cell.x, cell.y].Highlight(normalColor);
            }
        }
        highlightedCells.Clear();
    }

    private bool IsValidGridPosition(Vector2Int position)
    {
        return position.x >= 0 && position.x < gridWidth &&
               position.y >= 0 && position.y < gridHeight;
    }
}

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;

    private Vector2Int gridPosition;
    private RectTransform rectTransform;

    public Vector2Int Position => gridPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(Vector2Int position)
    {
        gridPosition = position;
    }

    public void Highlight(Color color)
    {
        backgroundImage.color = color;
    }

    public Vector2 GetWorldPosition()
    {
        return rectTransform.position;
    }
}

public class WeaponDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private GridInventoryManager inventoryManager;
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Vector2Int currentHoveredCell;
    private bool isDragging;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        // Get the current hovered grid position
        Vector2Int newHoveredCell = GetGridPosition(eventData.position);

        // If moved to a different cell, update preview
        if (newHoveredCell != currentHoveredCell)
        {
            currentHoveredCell = newHoveredCell;
            inventoryManager.ShowPlacementPreview(weaponData, currentHoveredCell);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        Vector2Int gridPosition = GetGridPosition(eventData.position);

        if (inventoryManager.CanPlaceWeapon(weaponData, gridPosition))
        {
            inventoryManager.PlaceWeapon(weaponData, gridPosition);
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }

        inventoryManager.ClearHighlights();
    }

    private Vector2Int GetGridPosition(Vector2 screenPosition)
    {
        // Raycast to find the slot under the cursor
        var results = new List<RaycastResult>();
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPosition;
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            InventorySlotUI slot = result.gameObject.GetComponent<InventorySlotUI>();
            if (slot != null)
            {
                return slot.Position;
            }
        }

        return new Vector2Int(-1, -1);
    }

    private void OnDisable()
    {
        if (isDragging)
        {
            inventoryManager.ClearHighlights();
        }
    }
}

// Helper extension for UI elements
public static class RectTransformExtensions
{
    public static Vector2Int GetGridPosition(this RectTransform rectTransform, Vector2 screenPosition, GridInventoryManager inventory)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            screenPosition,
            null,
            out Vector2 localPoint))
        {
            // Convert local point to grid position based on cell size
            float cellWidth = rectTransform.rect.width / inventory.GridWidth;
            float cellHeight = rectTransform.rect.height / inventory.GridHeight;

            int x = Mathf.FloorToInt((localPoint.x + rectTransform.rect.width / 2) / cellWidth);
            int y = Mathf.FloorToInt((localPoint.y + rectTransform.rect.height / 2) / cellHeight);

            return new Vector2Int(x, y);
        }

        return new Vector2Int(-1, -1);
    }
}