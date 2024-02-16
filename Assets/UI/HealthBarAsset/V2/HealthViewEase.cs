using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthViewEase : HealthView
{
    [SerializeField] private Slider _hardSlider;
    [SerializeField] private Slider _easeSlider;

    [SerializeField] private float _decreasingTime = 0.2f;
    [SerializeField] private float _decreasingSteps = 100f;
    [SerializeField] private float _decreasingLerpSpeed = 5f;

    private WaitForSeconds _waitDecrease;
    private Coroutine _easeDecreasing;
    private float _decreaseStep;

    private void Awake()
    {
        _hardSlider.maxValue = MaxHealth;
        _easeSlider.maxValue = MaxHealth;
        _waitDecrease = new WaitForSeconds(_decreasingTime);
        _decreaseStep = MaxHealth / _decreasingSteps;
    }

    private IEnumerator EaseDecreasing(float targetValue)
    {
        while (_easeSlider.value != targetValue)
        {
            // _easeSlider.value = Mathf.MoveTowards(_easeSlider.value, targetValue, _decreaseStep);
            _easeSlider.value = Mathf.Lerp(
                _easeSlider.value,
                targetValue,
                _decreasingLerpSpeed * Time.deltaTime);
            yield return _waitDecrease;
        }
    }

    public override void UpdateHealth(float targetValue)
    {
        if (targetValue >= _hardSlider.value)
            _easeSlider.value = targetValue;
        else
            _easeDecreasing = StartCoroutine(EaseDecreasing(targetValue));
        
        _hardSlider.value = targetValue;
    }
}
