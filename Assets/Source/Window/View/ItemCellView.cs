using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JustMobyTestTask
{
    public class ItemCellView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _stackCapacity;

        public void Initialize(ItemData itemData)
        {
            _icon.sprite = itemData.Sprite;
            _stackCapacity.text = itemData.StackCapacity.ToString();
        }
    }
}
