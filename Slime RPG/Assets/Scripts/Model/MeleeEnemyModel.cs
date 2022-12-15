using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Manager;
using Assets.Scripts.Enum;
using Assets.Scripts.UI;
using System;

namespace Assets.Scripts.Model
{
    public class MeleeEnemyModel : EnemyModel
    {
        public static event Action DeadAction;
        public bool IsDead => _isDead;

        [SerializeField] private Transform _spawnBulletTransform;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private float _stopDistance;
        private float _currentReloadTime = 0;
        private float _localMove;
        private SlimeModel _slimeModel;

        public bool _isDead = false;
        private void Start()
        {
            _healthBar.Setup(_hp);
            _localMove = _speedMove;
        }
        private void FixedUpdate()
        {
            if (_slimeModel == null)
                transform.position += Vector3.left * _speedMove * Time.deltaTime;
            else
            {
                if (_stopDistance <= Vector3.Distance(_slimeModel.transform.position, transform.position))
                    transform.position += (_slimeModel.transform.position - transform.position) * _speedMove * Time.deltaTime;
            }

            _currentReloadTime += Time.deltaTime;

            if (_currentReloadTime >= _reloadTime)
            {
                if (Physics.CheckSphere(transform.position, _sphereRadius, _enemyLayer))
                {
                    var hits = Physics.OverlapSphere(transform.position, _sphereRadius, _enemyLayer);

                    if (hits[0].transform.TryGetComponent<SlimeModel>(out SlimeModel ally))
                    {
                        _slimeModel = ally;
                        PoolManager.Instance.GetBullet(_spawnBulletTransform).SetupBullet(_slimeModel.transform.position - _spawnBulletTransform.transform.position, _damage);
                        //_speedMove = 0;
                        _currentReloadTime = 0;
                    }
                }
                else
                {
                    _speedMove = _localMove;
                }
            }
        }

        public override void UpdateHP(float damage)
        {
            _healthBar.UpdateHealthBar(damage);

            if (_hp <= 0)
            {
                _isDead = true;
                DeadAction?.Invoke();
                Destroy(gameObject);
            }
        }



        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _sphereRadius);
        }

        //public override void TakeDamage(int damage)
        //{
        //    _hp -= damage;
        //}
    }
}
