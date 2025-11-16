using UnityEngine;

public class TowerPurchaseButton : MonoBehaviour
{
    public GameObject towerPrefab;

    public void OnClickSelectTower()
    {
        // Check if the player can afford this tower
        TowerCost cost = towerPrefab.GetComponent<TowerCost>();
        if (cost != null && !CurrencyManager.Instance.SpendMoney(cost.cost))
        {
            Debug.Log("Not enough money to buy tower!");
            return;
        }

        // Find the grid parent
        RectTransform gridRect = GameObject.FindGameObjectWithTag("Grid")?.GetComponent<RectTransform>();
        if (gridRect == null)
        {
            Debug.LogError("No object with tag 'Grid' found!");
            return;
        }

        // Instantiate tower under grid
        GameObject newTower = Instantiate(towerPrefab, gridRect);
        RectTransform towerRect = newTower.GetComponent<RectTransform>();

        if (towerRect != null)
        {
            // Correct scale to match prefab visually inside parent
            Vector3 parentScale = gridRect.lossyScale;
            Vector3 prefabScale = towerPrefab.GetComponent<RectTransform>().localScale;
            towerRect.localScale = new Vector3(
                prefabScale.x / parentScale.x,
                prefabScale.y / parentScale.y,
                prefabScale.z / parentScale.z
            );

            // Position at mouse
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                gridRect,
                Input.mousePosition,
                null,
                out localPoint
            );
            towerRect.anchoredPosition = localPoint;
        }

        // Start drag logic
        TowerDragNDrop dragScript = newTower.GetComponent<TowerDragNDrop>();
        if (dragScript != null)
            dragScript.StartFromShop();
        else
            Debug.LogError("Tower prefab missing TowerDragNDrop component!");
    }
}
