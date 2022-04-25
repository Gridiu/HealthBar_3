using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private UnityEvent _healthChanging;

    private const float HealthMinValue = 0f;
    private const float HealthMaxValue = 100f;
    private const float HealthChange = 10f;

    private float _healthValue = 50f;

    private Coroutine _coroutine;

    public float HealthValue => _healthValue;

    public void TryIncreaseHealth()
    {
        if (_healthValue < HealthMaxValue)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ChangeHealth(_healthValue + HealthChange > HealthMaxValue ? HealthMaxValue : _healthValue + HealthChange));
        }
    }

    public void TryDecreaseHealth()
    {
        if (_healthValue > HealthMinValue)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ChangeHealth(_healthValue - HealthChange < HealthMinValue ? HealthMinValue : _healthValue - HealthChange));
        }
    }

    private IEnumerator ChangeHealth(float finishValue)
    {
        float changeStep = 10f;

        while (_healthValue != finishValue)
        {
            _healthValue = Mathf.MoveTowards(_healthValue, finishValue, changeStep * Time.deltaTime);

            _healthChanging.Invoke();

            yield return null;
        }
    }
}
