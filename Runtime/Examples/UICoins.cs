using TMPro;
using UnityEngine;

public class UICoins : MonoBehaviour
{
    private TextMeshProUGUI coinsText;

    public IntFact coinsFact;
    
    private void Awake()
    {
        coinsText = GetComponent<TextMeshProUGUI>();
        
        if(coinsFact.HasValue)
            OnCoinCollected(coinsFact.Value);
        
        coinsFact.onValueChanged += OnCoinCollected;
    }

    private void OnCoinCollected(int coinsCollected)
    {
        coinsText.SetText(coinsCollected.ToString());
    }

    private void OnDestroy()
    {
        coinsFact.onValueChanged -= OnCoinCollected;
    }
}