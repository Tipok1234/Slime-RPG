using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI;
using Assets.Scripts.Model;
using Assets.Scripts.Enum;

namespace Assets.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        private DataManager _dataManager;

        [SerializeField] private UpgradeWindow _upgradeWindow;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private SlimeModel _slimeModelPrefab;

        [SerializeField] private Transform _startPos;

        private SlimeModel _slimeModel;
        private void Awake()
        {
            _dataManager = FindObjectOfType<DataManager>();
        }

        private void Start()
        {
            _dataManager.LoadData();
            _upgradeWindow.Setup(_dataManager.CurrencyCount);
            _slimeModel = Instantiate(_slimeModelPrefab, _startPos);

            Debug.LogError("CASHICK" + _dataManager.CurrencyCount);

            _enemyManager.DeadUnitAction += OnDeadUnit;
            _slimeModel.DeadSlimeAction += OnDeadSlime;
            _upgradeWindow.UpgradeHPAction += OnUpgradeHP;
            _upgradeWindow.UpgradeDamageAction += OnUpgradeDamage;
            _upgradeWindow.UpgradeReloadAction += OnUpgradeReloadTime;

            _slimeModel.SetupCharacteristic(_dataManager.HP, _dataManager.Damage, _dataManager.ReloadTime);

            if (_dataManager.ReloadTime == 1)
            {
                _upgradeWindow.HideText();
            }


        }      

        private void OnDestroy()
        {
            _enemyManager.DeadUnitAction -= OnDeadUnit;
            _upgradeWindow.UpgradeHPAction -= OnUpgradeHP;
            _upgradeWindow.UpgradeDamageAction -= OnUpgradeDamage;
            _upgradeWindow.UpgradeReloadAction -= OnUpgradeReloadTime;
            _slimeModel.DeadSlimeAction -= OnDeadSlime;
        }

        private void OnDeadUnit()
        {
            _dataManager.AddCurrency(10);
            _upgradeWindow.Setup(_dataManager.CurrencyCount);
        }

        private void OnDeadSlime()
        {
            _upgradeWindow.RestartGame();
        }
        private void OnUpgradeHP()
        {
            if (_dataManager.CurrencyCount >= 150)
            {
                _dataManager.RemoveCurrency(150);
                _dataManager.UpdateCharacteristic(CharacteristicType.HP, 30);
                _upgradeWindow.Setup(_dataManager.CurrencyCount);
                _slimeModel.UpgradeStatic(CharacteristicType.HP);
            }
        }

        private void OnUpgradeDamage()
        {
            if (_dataManager.CurrencyCount >= 150)
            {
                _dataManager.RemoveCurrency(150);
                _dataManager.UpdateCharacteristic(CharacteristicType.Damage, 20);
                _upgradeWindow.Setup(_dataManager.CurrencyCount);
                _slimeModel.UpgradeStatic(CharacteristicType.Damage);
            }
        }

        private void OnUpgradeReloadTime()
        {
            if (_dataManager.ReloadTime == 1)
            {
                _upgradeWindow.HideText();
                return;
            }

            if (_dataManager.CurrencyCount >= 150)
            {
                _dataManager.RemoveCurrency(150);
                _dataManager.UpdateCharacteristic(CharacteristicType.ReloadTime, 1);
                _upgradeWindow.Setup(_dataManager.CurrencyCount);
                _slimeModel.UpgradeStatic(CharacteristicType.ReloadTime);
            }
        }
    }
}
