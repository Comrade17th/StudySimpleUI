using UnityEngine;

namespace Orts
{
    public abstract class HealthView : MonoBehaviour
    {
        [SerializeField] private Health _health;

        protected float MaxHealth => _health.MaxValue;

        private void Awake()
        {
            _health.ValueChanged += UpdateHealth;
        }

        public abstract void UpdateHealth(float targetValue);
    }
}