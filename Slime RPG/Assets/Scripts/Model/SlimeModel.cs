using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Manager;
using Assets.Scripts.UI;
using Assets.Scripts.Enum;
using TMPro;

namespace Assets.Scripts.Model
{
    public class SlimeModel : MonoBehaviour
    {
        public float Damage => _damage;
        public float HP => _hp;
        public float ReloadTime => _reloadTime;

        //public bool IsMaxUpgrade => _isMaxUpgrade;

        [SerializeField] private HealthBar _healthBar;

        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBulletTransform;

        [SerializeField] private float _speedMove;
        [SerializeField] private float _sphereRadius;

        private float _reloadTime;
        private float _hp;
        private float _damage;

        [SerializeField] private TMP_Text _hpText;

        private float _currentReloadTime = 0;
        private float _localMove;
        //private bool _isMaxUpgrade = false;

        private EnemyModel _enemyModel;
        private void Start()
        {
            _localMove = _speedMove;
        }

        private void FixedUpdate()
        {
            gameObject.transform.position += Vector3.right * _speedMove * Time.deltaTime;

            _currentReloadTime += Time.deltaTime;

            if (_currentReloadTime >= _reloadTime)
            {
                if (Physics.CheckSphere(transform.position, _sphereRadius, _enemyLayer))
                {
                    if (_enemyModel != null)
                    {
                        PoolManager.Instance.GetBullet(_spawnBulletTransform).SetupBullet(_enemyModel.transform.position - _spawnBulletTransform.transform.position, _damage);
                        _speedMove = 0;
                        _currentReloadTime = 0;
                    }
                    else
                    {
                        var hits = Physics.OverlapSphere(transform.position, _sphereRadius, _enemyLayer);

                        if (hits[0].transform.TryGetComponent<EnemyModel>(out EnemyModel enemy))
                        {
                            _enemyModel = enemy;
                            PoolManager.Instance.GetBullet(_spawnBulletTransform).SetupBullet(_enemyModel.transform.position - _spawnBulletTransform.transform.position, _damage);
                            _speedMove = 0;
                            _currentReloadTime = 0;
                        }

                    }
                }
                else
                {
                    _speedMove = _localMove;
                }
            }
        }

        public void UpdateHP(float damage)
        {
            _healthBar.UpdateHealthBar(damage);
            _hp -= damage;
            _hpText.text = _hp.ToString();

            if (_hp <= 0)
                Destroy(gameObject);

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _sphereRadius);
        }

        public void SetupCharacteristic(float hp, float damage, float reloadTime)
        {
            _hp = hp;
            _damage = damage;
            _reloadTime = reloadTime;
            _hpText.text = _hp.ToString();
            _healthBar.Setup(_hp);

            Debug.LogError("SLIME:     HP - DAMAGE --- TIME " + _hp + " " + _damage + " " + _reloadTime);
        }
        public void UpgradeStatic(CharacteristicType characteristicType)
        {
            switch (characteristicType)
            {
                case CharacteristicType.HP:
                    _healthBar.AddHealthBar(30);
                    _hp += 30;
                    _hpText.text = _hp.ToString();
                    _healthBar.Setup(_hp);
                    break;
                case CharacteristicType.Damage:
                    _damage += 20;
                    break;
                case CharacteristicType.ReloadTime:

                    //if (_reloadTime == 1)
                    //{
                    //    _isMaxUpgrade = true;
                    //    return;
                    //}

                    _reloadTime -= 1;
                    break;
            }
        }
    }
}