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
            return; // stop, do NOT spawn
        }

        GameObject gridParent = GameObject.FindGameObjectWithTag("Grid");
        if (gridParent == null)
        {
            Debug.LogError("No object with tag 'Grid' found!");
            return;
        }

        GameObject newTower = Instantiate(towerPrefab, gridParent.transform);
        RectTransform rect = newTower.GetComponent<RectTransform>();
        rect.localScale = Vector3.one;

        // Position the tower at the mouse position within the grid (BUG)
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            gridParent.GetComponent<RectTransform>(),
            Input.mousePosition,
            null,
            out localPoint
        );
        rect.anchoredPosition = localPoint;

        TowerDragNDrop dragScript = newTower.GetComponent<TowerDragNDrop>();
        if (dragScript != null)
        {
            dragScript.StartFromShop();
        }
        else
        {
            Debug.LogError("Tower prefab missing TowerDragNDrop component!");
        }
    }
}
