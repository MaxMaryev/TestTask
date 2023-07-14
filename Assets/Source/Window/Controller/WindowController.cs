using UnityEngine;
using UnityEngine.UI;

public class WindowController : MonoBehaviour
{
    [SerializeField] private Button _openWindowButton;
    [SerializeField] private InputField _stonesCountInput;
    [SerializeField] private InputField _woodCountInput;
    [SerializeField] private WindowModel _windowModel;

    private void OnEnable()
    {
        _openWindowButton.onClick.AddListener(OnBuyButtonClick);
        _stonesCountInput.onEndEdit.AddListener(OnItemsCountInputted);
        _woodCountInput.onEndEdit.AddListener(OnItemsCountInputted);
    }

    private void OnDisable()
    {
        _openWindowButton.onClick.RemoveListener(OnBuyButtonClick);
        _stonesCountInput.onEndEdit.RemoveListener(OnItemsCountInputted);
        _woodCountInput.onEndEdit.RemoveListener(OnItemsCountInputted);
    }

    private void OnBuyButtonClick()
    {
        _windowModel.gameObject.SetActive(true);
        _stonesCountInput.gameObject.SetActive(true);
        _woodCountInput.gameObject.SetActive(true);
    }

    private void OnItemsCountInputted(string input)
    {
        int.TryParse(_stonesCountInput.text, out int stonesCount);
        int.TryParse(_woodCountInput.text, out int woodCount);

        int totalItemsCount = stonesCount + woodCount;

        _windowModel.UpdateCellsCount(stonesCount, woodCount);
    }
}
