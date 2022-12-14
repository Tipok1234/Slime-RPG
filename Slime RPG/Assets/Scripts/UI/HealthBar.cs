using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Assets.Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _doTweenDuration;

        private float _maxHP;
        private float _currentHP;
        public void Setup(float maxHP)
        {
            _slider.value = 1;
            _maxHP = maxHP;
            _currentHP = _maxHP;
        }

        public void UpdateHealthBar(float damage)
        {
            _currentHP -= damage;

            float endValue = _currentHP <= 0 ? 0 : _currentHP / _maxHP;

            _slider.DOValue(endValue, _doTweenDuration);
        }

        public void AddHealthBar(float hp)
        {
            _currentHP += hp;

            float endValue = _currentHP <= 0 ? 0 : _currentHP / _maxHP;

            _slider.DOValue(endValue, _doTweenDuration);
        }
    }
}
