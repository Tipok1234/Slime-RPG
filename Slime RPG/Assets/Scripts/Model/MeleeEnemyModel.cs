using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Manager;
using Assets.Scripts.Enum;

namespace Assets.Scripts.Model
{
    public class MeleeEnemyModel : EnemyModel
    {
        [SerializeField] private Transform _spawnBulletTransform;

        private float _currentReloadTime = 0;
        private float _localMove;
        private SlimeModel _slimeModel;
        private void Start()
        {
            _localMove = _speedMove;
        }
        private void FixedUpdate()
        {
            gameObject.transform.position += Vector3.left * _speedMove * Time.deltaTime;

            _currentReloadTime += Time.deltaTime;

            if (_currentReloadTime >= _reloadTime)
            {
                if (Physics.CheckSphere(transform.position, _sphereRadius, _enemyLayer))
                {
                   var hits = Physics.OverlapSphere(transform.position, _sphereRadius, _enemyLayer);

                    for (int i = 0; i < hits.Length; i++)
                    {
                        if (hits[i].transform.TryGetComponent<SlimeModel>(out SlimeModel ally))
                        {
                            _slimeModel = ally;
                            PoolManager.Instance.GetBullet(_spawnBulletTransform).SetupBullet(_slimeModel.transform.position - _spawnBulletTransform.transform.position);
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _sphereRadius);
        }
    }
}
