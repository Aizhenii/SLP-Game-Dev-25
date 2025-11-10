
using UnityEngine;
using UnityEngine.UI;

public class TowerPurchaseButton : MonoBehaviour
{
    public GameObject towerPrefab; // assign the tower prefab in inspector

    public void OnClickSelectTower()
    {
        // tell the DragManager which tower prefab to spawn
        DragManager.Instance.SetSelectedTowerPrefab(towerPrefab);
    }
}
