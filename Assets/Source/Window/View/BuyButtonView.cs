using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Image _discountIcon; 
    [SerializeField] private TextMeshProUGUI _discount;

    public void DisplayPrice(float price, float discount)
    {
        if (discount == 0)
        {
            _price.text = price.ToString();
            _discountIcon.gameObject.SetActive(false);
        }
        else
        {
            float discountPrice = price * (100 - discount) / 100;
            float fontSize = _price.fontSize;
            _price.text = $"${discountPrice:0.00}\n<color=#D9D9D9><size={fontSize*0.95f}><s>${price}</s></size></color>";
            _discount.text = $"-{discount}%";
            _discountIcon.gameObject.SetActive(true);
        }
    }
}
