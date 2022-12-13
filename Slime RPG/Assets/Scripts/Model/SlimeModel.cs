using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Manager;
using Assets.Scripts.UI;
using Assets.Scripts.Enum;

namespace Assets.Scripts.Model
{
    public class SlimeModel : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;

        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Transform _spawnBulletTransform;

        [SerializeField] private float _speedMove;
        [SerializeField] private float _sphereRadius;
        [SerializeField] private float _reloadTime;
        [SerializeField] private int _hp;

        private float _currentReloadTime = 0;
        private float _localMove;
        private EnemyModel _enemyModel;
        private void Start()
        {
            _healthBar.Setup(_hp);
            _localMove = _speedMove;
        }

        private void FixedUpdate()
        {
            gameObject.transform.position += Vector3.right * _speedMove * Time.deltaTime;

            _currentReloadTime += Time.deltaTime;

            if(_currentReloadTime >= _reloadTime)
            {
                if(Physics.CheckSphere(transform.position,_sphereRadius,_enemyLayer))
                {
                    var hits = Physics.OverlapSphere(transform.position,_sphereRadius,_enemyLayer);

                    for (int i = 0; i < hits.Length; i++)
                    {
                        if(hits[i].transform.TryGetComponent<EnemyModel>(out EnemyModel enemy))
                        {
                            _enemyModel = enemy;

                            PoolManager.Instance.GetBullet(_spawnBulletTransform).SetupBullet(_enemyModel.transform.position - _spawnBulletTransform.transform.position);
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
            Gizmos.DrawWireSphere(transform.position,_sphereRadius);
        }
    }
}
