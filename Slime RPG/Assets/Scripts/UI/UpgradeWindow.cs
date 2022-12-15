using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace Assets.Scripts.UI
{
    public class UpgradeWindow : MonoBehaviour
    {
        public event Action UpgradeHPAction;
        public event Action UpgradeDamageAction;
        public event Action UpgradeReloadAction;


        [SerializeField] private TMP_Text _countCashText;
        [SerializeField] private TMP_Text _maxUpgradeText;
        [SerializeField] private TMP_Text _upgradeText;

        [SerializeField] private Button _upgradeHPButton;
        [SerializeField] private Button _upgradeDamageButton;
        [SerializeField] private Button _upgradeReloadTimeButton;
        [SerializeField] private Button _restartGameButton;

        private void Awake()
        {
            Time.timeScale = 1;

            _upgradeHPButton.onClick.AddListener(UpgradeHP);
            _upgradeDamageButton.onClick.AddListener(UpgradeDamage);
            _upgradeReloadTimeButton.onClick.AddListener(UpgradeReloadTime);
            _restartGameButton.onClick.AddListener(RestartGameOnClick);

            _upgradeText.text = "-" +1.ToString();
        }

        public void RestartGame()
        {
            _restartGameButton.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        private void RestartGameOnClick()
        {
            _restartGameButton.gameObject.SetActive(false);
            SceneManager.LoadScene("SampleScene");
        }
        public void Setup(int currency)
        {
            _countCashText.text = "CASH: " + currency.ToString();
        }
        private void UpgradeHP()
        {
            UpgradeHPAction?.Invoke();
        }

        private void UpgradeDamage()
        {
            UpgradeDamageAction?.Invoke();
        }

        private void UpgradeReloadTime()
        {
            UpgradeReloadAction?.Invoke();
        }

        public void HideText()
        {
            _maxUpgradeText.enabled = true;
            _upgradeText.enabled = false;
        }
    }
}
