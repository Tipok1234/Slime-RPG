using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enum;

namespace Assets.Scripts.Model
{
    public class BulletModel : MonoBehaviour
    {
        [SerializeField] private float _speedBullet;
        [SerializeField] private float _lifeTimeBullet;
        private float _currentTimeBullet;

        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private LayerMask _allyLayer;

        private bool _isActive;
        private Vector3 _direction;

        private float _damage;
        private void FixedUpdate()
        {
            if (_isActive)
            {
                transform.position += _direction * _speedBullet * Time.deltaTime;
                _currentTimeBullet += Time.deltaTime;

                Debug.DrawLine(transform.position, _direction * (0.3f), Color.red);

                var ray = new Ray(transform.position, _direction * (0.3f));

                if (Physics.Raycast(ray, out RaycastHit hit, 0.15f, _enemyLayer))
                {
                    if (hit.transform.TryGetComponent<EnemyModel>(out EnemyModel enemy))
                    {
                        ResetBullet();
                        enemy.TakeDamage(_damage);
                        enemy.UpdateHP(_damage);
                    }
                }

                if (Physics.Raycast(ray, out RaycastHit hit1, 0.15f, _allyLayer))
                {
                    if (hit1.transform.TryGetComponent<SlimeModel>(out SlimeModel ally))
                    {
                        ally.UpdateHP(_damage);
                        ResetBullet();
                    }
                }
            }
        }

        private void ResetBullet()
        {
            _isActive = false;
            _currentTimeBullet = 0;
            gameObject.SetActive(false);
        }

        public void SetupBullet(Vector3 direction, float damage)
        {
            _direction = direction.normalized;
            _direction.y = 0;
            _isActive = true;
            _damage = damage;
        }
    }
}
