using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour
{
    public static DragManager Instance;

    private GameObject towerPreview;       // preview being dragged
    private GameObject selectedTowerPrefab; // prefab selected from shop
    private RectTransform canvasRect;
    private Canvas mainCanvas;

    private void Awake()
    {
        Instance = this;
        mainCanvas = GetComponent<Canvas>();
        canvasRect = mainCanvas.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (towerPreview != null)
        {
            FollowMouse();

            if (Input.GetMouseButtonDown(0)) // place on left-click
                PlaceTower();

            if (Input.GetMouseButtonDown(1)) // cancel on right-click
                CancelPlacement();
        }
    }

    public void SetSelectedTowerPrefab(GameObject prefab)
    {
        CancelPlacement();
        selectedTowerPrefab = prefab;

        towerPreview = Instantiate(selectedTowerPrefab, canvasRect);
        towerPreview.transform.localScale = Vector3.one;

        var cg = towerPreview.GetComponent<CanvasGroup>();
        if (!cg) cg = towerPreview.AddComponent<CanvasGroup>();
        cg.alpha = 0.6f;
        cg.blocksRaycasts = false; // so raycasts go through to grid
    }

    private void FollowMouse()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Input.mousePosition,
            mainCanvas.worldCamera,
            out Vector2 localPoint
        );

        towerPreview.GetComponent<RectTransform>().anchoredPosition = localPoint;
    }

    private void PlaceTower()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        var raycastResults = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        foreach (var r in raycastResults)
        {
            if (r.gameObject.CompareTag("Grid") && r.gameObject.transform.childCount == 0)
            {
                towerPreview.transform.SetParent(r.gameObject.transform, false);
                towerPreview.transform.localPosition = Vector3.zero;
                var cg = towerPreview.GetComponent<CanvasGroup>();
                cg.alpha = 1f;
                cg.blocksRaycasts = true;
                towerPreview = null;
                selectedTowerPrefab = null;
                return;
            }
        }

        // If no valid tile, cancel placement
        CancelPlacement();
    }

    private void CancelPlacement()
    {
        if (towerPreview != null)
            Destroy(towerPreview);

        towerPreview = null;
        selectedTowerPrefab = null;
    }
}
