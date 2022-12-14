using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enum;

namespace Assets.Scripts.Manager
{
    public class DataManager : MonoBehaviour
    {
        public int CurrencyCount => _currencyCount;
        public float HP => _countHP;
        public float Damage => _countDamage;
        public float ReloadTime => _reloadTime;

        private int _currencyCount;
        private float _countHP;
        private float _countDamage;
        private float _reloadTime;

        private string _currencyKey = "CurrencyKey";
        private string _hpKey = "HPKey";
        private string _damageKey = "DamageKey";
        private string _reloadTimeKey = "ReloadTimeKey";

        public void LoadData()
        {
            _currencyCount = PlayerPrefs.GetInt(_currencyKey, 1000);

            SetupCharacteristic();
            Debug.LogError("CASH: " + _currencyCount);
        }
        private void SetupCharacteristic()
        {
            _countHP = PlayerPrefs.GetFloat(_hpKey, 100);
            _countDamage = PlayerPrefs.GetFloat(_damageKey, 10);
            _reloadTime = PlayerPrefs.GetFloat(_reloadTimeKey, 5);

            Debug.LogError("HP - DAMAGE --- TIME " + _countHP + " " + _countDamage + " " + _reloadTime);
        }

        public void UpdateCharacteristic(CharacteristicType characteristicType, float count)
        {
            switch (characteristicType)
            {
                case CharacteristicType.HP:
                    _countHP += count;
                    PlayerPrefs.SetFloat(_hpKey, _countHP);
                    break;
                case CharacteristicType.Damage:
                    _countDamage += count;
                    PlayerPrefs.SetFloat(_damageKey, _countDamage);
                    break;
                case CharacteristicType.ReloadTime:

                    if (_reloadTime == 1)
                        return;

                    _reloadTime -= count;
                    PlayerPrefs.SetFloat(_reloadTimeKey, _reloadTime);
                    break;
            }
        }
        public void AddCurrency(int currency)
        {
            _currencyCount += currency;
            PlayerPrefs.SetInt(_currencyKey, _currencyCount);
        }

        public void RemoveCurrency(int currency)
        {
            _currencyCount -= currency;
            PlayerPrefs.SetInt(_currencyKey, _currencyCount);
        }
    }
}
