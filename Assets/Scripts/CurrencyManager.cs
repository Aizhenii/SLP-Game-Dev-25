using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int currency = 200;
    public TextMeshProUGUI currencyText;

    private void Awake() => Instance = this;

    private void Start() => UpdateUI();

    public void AddMoney(int amount)
    {
        currency += amount;
        UpdateUI();
    }

    public bool SpendMoney(int amount)
    {
        if (currency < amount) return false;
        currency -= amount;
        UpdateUI();
        return true;
    }

    private void UpdateUI()
    {
        currencyText.text = currency.ToString();
    }
}
