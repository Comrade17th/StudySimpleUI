using System;
using UnityEngine;

namespace Orts
{
    public class Health : MonoBehaviour
    {
        [field: SerializeField] public float MaxValue { get; private set; }

        public event Action<float> ValueChanged;

        private float _currentValue;

        private void Awake()
        {
            _currentValue = MaxValue;
        }

        public void TakeDamage(float amount)
        {
            _currentValue -= amount;

            _currentValue = Mathf.Clamp(_currentValue, 0, MaxValue);
            ValueChanged?.Invoke(_currentValue);
        }

        public void Heal(float amount)
        {
            _currentValue += amount;
            _currentValue = Mathf.Clamp(_currentValue, 0, MaxValue);

            ValueChanged?.Invoke(_currentValue);
        }
    }
}