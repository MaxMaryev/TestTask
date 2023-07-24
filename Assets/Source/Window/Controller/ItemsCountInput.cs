using UnityEngine.UI;
using UnityEngine;
using System;

namespace JustMobyTestTask
{
    [RequireComponent(typeof(InputField))]
    public class ItemsCountInput : MonoBehaviour
    {
        private InputField _inputField;

        public Action Done;

        [field: SerializeField] public ItemName ItemName { get; private set; }
        public int Value { get; private set; }

        private void Awake() => _inputField = GetComponent<InputField>();

        private void OnEnable() => _inputField.onEndEdit.AddListener(OnInputted);

        private void OnDisable() => _inputField.onEndEdit.RemoveListener(OnInputted);

        private void OnInputted(string input)
        {
            Value = Convert.ToInt32(input);
            Done?.Invoke();
        }
    }
}
